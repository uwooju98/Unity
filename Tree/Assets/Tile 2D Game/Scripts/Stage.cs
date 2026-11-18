using System.Collections.Generic;
using UnityEngine;


public class Stage : MonoBehaviour
{
    public GameObject tilePrefab;

    public Player playerPrefab;
    private Player player;

    public GameObject[] tileObjs;

    public MapSettings mapSettings = new MapSettings();

    public int mapWidth = 20;
    public int mapHeight = 20;


    public SearchType searchType;

    public Vector2 tileSize = new Vector2(16, 16);

    public Sprite[] islandSprites;
    private Map map;
    public Map Map => map;

    private PathFinder pathFinder = new PathFinder();

    private void Awake()
    {
        ResetStage();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var tileId = ScreenPosTileId(Input.mousePosition, false);

            if (tileId >= 0 && tileId < map.tiles.Length)
            {
                if (map.tiles[tileId].CanVisit)
                {
                    map.startTile = map.tiles[tileId];
                    var tileSprite = tileObjs[tileId].GetComponent<SpriteRenderer>();

                    tileSprite.color = Color.red;
                    Debug.Log($"출발 타일: {map.startTile.id}");
                }
                else
                {
                    Debug.Log("지정할 수 없는 타일");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var tileId = ScreenPosTileId(Input.mousePosition, false);

            if (tileId >= 0 && tileId < map.tiles.Length)
            {
                if (map.tiles[tileId].CanVisit)
                {
                    map.endTile = map.tiles[tileId];
                    Debug.Log($"도착 타일: {map.endTile.id}");
                    var tileSprite = tileObjs[tileId].GetComponent<SpriteRenderer>();

                    tileSprite.color = Color.green;
                }
                else
                {
                    Debug.Log("지정할 수 없는 타일");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Clearplayer();
            StartSelectedSearch();
        }

    }

    private void StartSelectedSearch()
    {
        ClearPath();

        if (map.startTile == null || map.endTile == null)
        {
            return;
        }

        bool success = false;
        switch (searchType)
        {
            case SearchType.DFS:
                success = pathFinder.DFS(map.startTile, map.endTile);
                break;
            case SearchType.BFS:
                success = pathFinder.BFS(map.startTile, map.endTile);
                break;
            case SearchType.AStar:
                success = pathFinder.AStar(map.startTile, map.endTile, mapWidth);
                break;
        }

        Debug.Log(success ? "성공" : "실패");

        if (success)
        {
            ShowPath();

            CreatePlayer();
            var pathPosition = GetPathPositions();
            StartCoroutine(player.MovePath(pathPosition));
        }
    }



    [ContextMenu("Start Search")]
    public void StartSearch()
    {
        ClearPath();

        map.SelectPathPoints();

        bool success = false;
        switch (searchType)
        {
            case SearchType.DFS:
                success = pathFinder.DFS(map.startTile, map.endTile);
                break;
            case SearchType.BFS:
                success = pathFinder.BFS(map.startTile, map.endTile);
                break;
            case SearchType.AStar:
                success = pathFinder.AStar(map.startTile, map.endTile, mapWidth);
                break;
        }

        Debug.Log(success ? "성공" : "실패");

        if (success)
        {
            ShowPath();

            CreatePlayer();
            var pathPosition = GetPathPositions();
            StartCoroutine(player.MovePath(pathPosition));
        }
    }

    [ContextMenu("Reset Stage")]
    public void ResetStage()
    {
        Clearplayer();
        ClearPath();

        map = new Map();
        map.Init(mapHeight, mapWidth);


        map.CreateIsland(mapSettings);
        map.CreateObject(mapSettings);
        map.SetWeights();


        CreateGrid();
    }

    private void CreateGrid()
    {
        if (tileObjs != null)
        {
            foreach (var tile in tileObjs)
            {
                Destroy(tile.gameObject);
            }
        }

        tileObjs = new GameObject[mapWidth * mapHeight];

        Vector3 pos = Vector3.zero;
        float startX = pos.x;

        for (int i = 0; i < mapHeight; ++i)
        {
            for (int j = 0; j < mapWidth; ++j)
            {
                var tileId = i * mapWidth + j;
                var tile = map.tiles[tileId];

                var newGo = Instantiate(tilePrefab, transform);

                newGo.transform.position = pos;
                pos.x += tileSize.x;

                newGo.name = $"Tile {tileId} ({i}, {j})";
                tileObjs[tileId] = newGo;

                DecorateTile(tileId);
            }

            pos.x = startX;
            pos.y -= tileSize.y;
        }
    }

    public void DecorateTile(int tileId)
    {
        var tile = map.tiles[tileId];
        var tileGo = tileObjs[tileId];
        var ren = tileGo.GetComponent<SpriteRenderer>();

        if (tile.autoTileId != (int)TileTypes.Empty)
        {
            ren.sprite = islandSprites[tile.autoTileId];
        }
        else
        {
            ren.sprite = null;
        }
    }

    private void CreatePlayer()
    {
        Clearplayer();

        var original = playerPrefab;
        var position = tileObjs[map.startTile.id].transform.position;
        var rotation = Quaternion.identity;

        player = Instantiate(original, position, rotation);
    }

    private void Clearplayer()
    {
        StopAllCoroutines();

        if (player != null)
        {
            Destroy(player.gameObject);
            player = null;
        }
    }

    private List<Vector3> GetPathPositions()
    {
        List<Vector3> paths = new List<Vector3>();

        foreach (var path in pathFinder.path)
        {
            var pathPosition = tileObjs[path.id].transform.position;
            paths.Add(pathPosition);
        }

        return paths;
    }

    private int ScreenPosTileId(Vector3 screenPos, bool clamp = true)
    {
        Camera cam = Camera.main;
        screenPos.z = Mathf.Abs(transform.position.z - cam.transform.position.z);

        return WorldPosToTileId(Camera.main.ScreenToWorldPoint(screenPos), clamp);
    }

    private int WorldPosToTileId(Vector3 wolrdPos, bool clamp = true)
    {
        var startPos = tileObjs[0].transform.position;

        int x = Mathf.FloorToInt((wolrdPos.x - startPos.x) / tileSize.x + 0.5f);
        int y = Mathf.FloorToInt((startPos.y - wolrdPos.y) / tileSize.y + 0.5f);

        if (clamp)
        {
            x = Mathf.Clamp(x, 0, mapWidth - 1);
            y = Mathf.Clamp(y, 0, mapHeight - 1);
        }

        return y * mapWidth + x;
    }

    private void ShowPath()
    {
        for (int i = 0; i < pathFinder.path.Count; i++)
        {
            var tile = pathFinder.path[i];

            float t = pathFinder.path.Count > 1
                ? (float)i / (pathFinder.path.Count - 1)
                : 0f;

            var color = Color.Lerp(Color.red, Color.green, t);

            var tileSprite = tileObjs[tile.id].GetComponent<SpriteRenderer>();

            tileSprite.color = color;
        }
    }

    private void ClearPath()
    {
        foreach(var tile in pathFinder.path)
        {
            var tileSprite = tileObjs[tile.id].GetComponent<SpriteRenderer>();
            tileSprite.color = Color.white;
        }
    }
}