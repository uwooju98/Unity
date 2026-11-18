using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public float moveSpeed = 100f;

    private Tile currentTile;
    private Stage stage;
    private Animator animator;

    private bool isMoving;

    private bool isSelectStart = false;
    private bool isSelectCastle = false;

    private List<Tile> paths;

    private Tile pendingTarget;


    private void Start()
    {
        var mapObject = GameObject.FindWithTag("Map");
        if (mapObject != null)
        {
            stage = mapObject.GetComponent<Stage>();
        }

        currentTile = stage.Map.startTile;
        transform.position = stage.GetTilePos(currentTile.id);
        stage.OnTileVisited(currentTile.id);
        animator = GetComponent<Animator>();
        animator.speed = 0f;
    }


    private void Update()
    {
        if (isMoving || currentTile == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectStartTile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectCastleTile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(CoSelectMove());
        }

        //int direction = -1;

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    direction = (int)Sides.Top;
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    direction = (int)Sides.Bottom;
        //}
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    direction = (int)Sides.Left;
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    direction = (int)Sides.Right;
        //}

        //if (direction < 0)
        //{
        //    return;
        //}

        //var next = currentTile.adjacents[direction];

        //if (next == null || !next.CanMove)
        //{
        //    return;
        //}
        //StartCoroutine(CoMoveOneStep(next));
    }


    private void SelectStartTile()
    {
        var tileId = stage.ScreenPosTileId(Input.mousePosition, false);
        var target = stage.Map.tiles[tileId];

        if(!target.CanMove)
        {
            return;
        }

        pendingTarget = target;

        if (!isMoving)
        {
            StartCoroutine(CoSelectMove());
        }



        //if (isSelectStart && stage.Map.tiles[tileId].CanMove)
        //{
        //    var tileSprite = stage.tileObjs[stage.Map.startTile.id].GetComponent<SpriteRenderer>();
        //    tileSprite.color = Color.white;
        //    isSelectStart = false;
        //    Debug.Log("시작 타일 색상 초기화");
        //}

        //if (tileId >= 0 && tileId < stage.Map.tiles.Length)
        //{
        //    if (stage.Map.tiles[tileId].CanMove)
        //    {
        //        stage.Map.startTile = stage.Map.tiles[tileId];
        //        currentTile = stage.Map.startTile;
        //        ClearTileColor();
        //        positionReset();
        //        var tileSprite = stage.tileObjs[tileId].GetComponent<SpriteRenderer>();

        //        tileSprite.color = Color.red;
        //        Debug.Log($"출발 타일: {stage.Map.startTile.id}");

        //        isSelectStart = true;
        //    }
        //    else
        //    {
        //        Debug.Log("지정할 수 없는 타일");
        //    }
        //}
    }


    private void SelectCastleTile()
    {
        var tileId = stage.ScreenPosTileId(Input.mousePosition, false);

        if (isSelectCastle && stage.Map.tiles[tileId].CanMove)
        {
            var tileSprite = stage.tileObjs[stage.Map.castleTile.id].GetComponent<SpriteRenderer>();
            tileSprite.color = Color.white;
            isSelectStart = false;
            Debug.Log("시작 타일 색상 초기화");
        }

        if (tileId >= 0 && tileId < stage.Map.tiles.Length)
        {
            if (stage.Map.tiles[tileId].CanMove)
            {
                stage.Map.castleTile = stage.Map.tiles[tileId];
                ClearTileColor();
                var tileSprite = stage.tileObjs[tileId].GetComponent<SpriteRenderer>();
                tileSprite.color = Color.green;
                Debug.Log($"도착 타일: {stage.Map.castleTile.id}");

                isSelectCastle = true;
            }
            else
            {
                Debug.Log("지정할 수 없는 타일");
            }
        }
    }


    private void positionReset()
    {
        transform.position = stage.tileObjs[stage.Map.startTile.id].transform.position;
    }


    private IEnumerator CoSelectMove()
    {
        isMoving = true;
        animator.speed = 1f;

        List<Tile> path = null;
        Tile pathTarget = null;

        int pathIndex = 0;

        while (pendingTarget != currentTile)
        {
            if (pathTarget != pendingTarget)
            {
                path = stage.Map.PathFinding(currentTile, pendingTarget);
                pathTarget = pendingTarget;
                pathIndex = 1;
            }

            if (path == null || pathIndex >= path.Count)
            {
                break;
            }

            yield return StartCoroutine(CoMoveOneStep(path[pathIndex++]));
        }

        //positionReset();
        //paths = stage.Map.PathFinding(stage.Map.startTile, stage.Map.castleTile);

        //if (!(paths == null))
        //{
        //    ClearTileColor();
        //    ShowPathColor();
        //    foreach (var path in paths)
        //    {
        //        Debug.Log($"id {path.id} weight{path.Weight}");
        //        yield return StartCoroutine(CoMoveOneStep(path));
        //    }
        //}
        //else
        //{
        //    Debug.Log("경로가 없어요");
        //}

        isMoving = false;
        animator.speed = 0f;
    }


    private IEnumerator CoMoveOneStep(Tile next)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = stage.GetTilePos(next.id);
        float moveDuration = Vector3.Distance(startPos, targetPos) / moveSpeed;

        var elapse = 0f;

        while(elapse < moveDuration)
        {
            elapse += Time.deltaTime;
            float t = elapse / moveDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }


        transform.position = targetPos;
        currentTile = next;
        
        stage.OnTileVisited(currentTile.id);
    }

    private void ShowPathColor()
    {
        for (int i = 0; i < paths.Count; i++)
        {
            var tile = paths[i];

            float t = paths.Count > 1
                ? (float)i / (paths.Count - 1)
                : 0f;

            var color = Color.Lerp(Color.red, Color.green, t);

            var tileSprite = stage.tileObjs[tile.id].GetComponent<SpriteRenderer>();

            tileSprite.color = color;
        }
    }

    private void ClearTileColor()
    {
        foreach (var tile in stage.Map.tiles)
        {
            if (tile == stage.Map.startTile || tile == stage.Map.castleTile)
            {
                continue;
            }
            var tileSprite = stage.tileObjs[tile.id].GetComponent<SpriteRenderer>();
            tileSprite.color = Color.white;
        }
    }
}