using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

    [ContextMenu("Start Search")]
    public void StartSearch()
    {
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
            CreatePlayer();
            var pathPosition = GetPathPositions();
            StartCoroutine(player.MovePath(pathPosition));
        }
    }

    [ContextMenu("Reset Stage")]
    public void ResetStage()
    {
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
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        var original = playerPrefab;
        var position = tileObjs[map.startTile.id].transform.position;
        var rotation = Quaternion.identity;

        player = Instantiate(original, position, rotation);
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
}