using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 100f;

    [SerializeField]
    private int visionRange = 2;

    private Stage stage;
    private Animator animator;

    private Tile currentTile;
    private Tile pendingTarget;

    private bool isMoving;

    private void Start()
    {
        var mapObject = GameObject.FindWithTag("Map");
        if (mapObject != null)
        {
            stage = mapObject.GetComponent<Stage>();
        }

        currentTile = stage.Map.startTile;
        transform.position = stage.GetTilePos(currentTile.id);
        stage.OnTileVisited(currentTile.id, visionRange);
        animator = GetComponent<Animator>();
        animator.speed = 0f;
    }

    private void Update()
    {
        if (isMoving || currentTile == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectTargetTile();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(CoMoveToTarget());
        }

    }

    private void SelectTargetTile()
    {
        var tileId = stage.ScreenPosTileId(Input.mousePosition, false);
        var target = stage.Map.tiles[tileId];

        if (!target.CanMove)
        {
            return;
        }

        pendingTarget = target;

        if (!isMoving)
        {
            StartCoroutine(CoMoveToTarget());
        }

    }

    private IEnumerator CoMoveToTarget()
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

        isMoving = false;
        animator.speed = 0f;
    }

    private IEnumerator CoMoveOneStep(Tile next)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = stage.GetTilePos(next.id);
        float moveDuration = Vector3.Distance(startPos, targetPos) / moveSpeed;

        var elapse = 0f;

        while (elapse < moveDuration)
        {
            elapse += Time.deltaTime;
            float t = elapse / moveDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        transform.position = targetPos;
        currentTile = next;

        stage.OnTileVisited(currentTile.id, visionRange);
    }
}
