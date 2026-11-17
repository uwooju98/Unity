using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject[] tileObjs;

    public int mapWidth = 20;
    public int mapHeight = 20;

    public Vector2 tileSize = new Vector2(16, 16);

    public Sprite[] islandSprites;

    private Map map;

    public Map Map => map;

    // 추가: 해안선 침식 비율
    [Range(0f, 1f)]
    public float erodePercent = 0.3f;

    // 추가: 침식 반복 횟수
    public float erodeIterations = 2;

    // 추가: 호수 생성 비율
    [Range(0f, 1f)]
    public float lakePercent = 0.1f;
    public float TreePercent = 0.1f;

    [ContextMenu("Reset Stage")]
    public void ResetStage()
    {
        map = new Map();
        map.Init(mapHeight, mapWidth);

        // 추가: 섬 생성
        map.CreateIsland(
            erodePercent,
            erodeIterations,
            lakePercent);
        map.CreateTree(erodePercent,
            erodeIterations,
            TreePercent);
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
}