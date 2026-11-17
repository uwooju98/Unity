using System.Linq;
using UnityEngine; // 추가
public enum TileTypes
{
    Empty = -1,

    // 0 ~ 14 해안선
    // 15 땅
    Grass = 15,
    Tree = 16,
    Hills = 17,
    Mountains = 18,
    Towns = 19, // 마을
    Castles = 20, //성 1개만
    Monster = 21, //던전
}

public class Map
{
    public int rows = 0;
    public int cols = 0;

    public Tile[] tiles;

    public Tile[] CoastTiles =>
        tiles.Where(t => t.autoTileId >= 0 &&
        t.autoTileId < (int)TileTypes.Grass).ToArray();

    public Tile[] LandTiles =>
        tiles.Where(t => t.autoTileId >=
        (int)TileTypes.Grass).ToArray();

    public Tile[] GrassTiles => tiles
    .Where(t => t.autoTileId == (int)TileTypes.Grass)
    .ToArray();

    public Tile[] CastlesTiles => tiles
    .Where(t => t.autoTileId == (int)TileTypes.Castles)
    .ToArray();

    public Tile[] TownsTiles => tiles
    .Where(t => t.autoTileId == (int)TileTypes.Towns)
    .ToArray();

    public Tile startTile;
    public Tile endTile;



    public void Init(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;

        tiles = new Tile[rows * cols];

        for (int i = 0; i < tiles.Length; ++i)
        {
            tiles[i] = new Tile { id = i };
        }

        for (int r = 0; r < rows; ++r)
        {
            // 수정: rows → cols
            for (int c = 0; c < cols; ++c)
            {
                int index = r * cols + c;
                var adjacents = tiles[index].adjacents;

                if ((r - 1) >= 0)
                {
                    adjacents[(int)Sides.Top] =
                        tiles[index - cols];
                }

                if ((c + 1) < cols)
                {
                    adjacents[(int)Sides.Right] =
                        tiles[index + 1];
                }

                if ((c - 1) >= 0)
                {
                    adjacents[(int)Sides.Left] =
                        tiles[index - 1];
                }

                if ((r + 1) < rows)
                {
                    adjacents[(int)Sides.Bottom] =
                        tiles[index + cols];
                }
            }
        }

        for (int i = 0; i < tiles.Length; ++i)
        {
            tiles[i].UpdateAutoTileId();
        }
    }

    public void CreateObject(MapSettings mapSettings)
    {
        DecorateTiles(GrassTiles, mapSettings.castlesCount, TileTypes.Castles);
        DecorateTiles(GrassTiles, mapSettings.townsCount, TileTypes.Towns);

        DecorateTiles(GrassTiles, mapSettings.treePercent, TileTypes.Tree);
        DecorateTiles(GrassTiles, mapSettings.hillsPercent, TileTypes.Hills);
        DecorateTiles(GrassTiles, mapSettings.mountainsPercent, TileTypes.Mountains);
        DecorateTiles(GrassTiles, mapSettings.monsterPercent, TileTypes.Monster);
    }

    // 추가: 섬 생성
    public bool CreateIsland(MapSettings mapSettings)
    {
        // 육지 내부에 호수 생성
        DecorateTiles(LandTiles, mapSettings.lakePercent, TileTypes.Empty);

        // 해안선 침식
        for (int i = 0; i < mapSettings.erodeIterations; ++i)
        {
            DecorateTiles(CoastTiles, mapSettings.erodePercent, TileTypes.Empty);
        }

        return true;
    }

    // 추가: 타일 타입 변경
    public void DecorateTiles(
        Tile[] tile, float percent, TileTypes tileType)
    {
        int total = Mathf.FloorToInt(tile.Length * percent);

        DecorateTiles(tile, total, tileType);
    }

    public void DecorateTiles(
        Tile[] tile, int count, TileTypes tileType)
    {
        ShuffleTiles(tile);
        for (int i = 0; i < count; ++i)
        {
            if (tileType == TileTypes.Empty)
            {
                tile[i].ClearAdjacents();
            }
            tile[i].autoTileId = (int)tileType;
        }
    }


    public void ShuffleTiles(Tile[] tiles)
    {
        for (int i = tiles.Length - 1; i > 0; --i)
        {
            int index = Random.Range(0, i + 1);

            Tile temp = tiles[i];
            tiles[i] = tiles[index];
            tiles[index] = temp;
        }
    }

    public void SelectPathPoints()
    {
        var towns = TownsTiles;
        var castles = CastlesTiles;

        startTile = towns[Random.Range(0, towns.Length)];
        endTile = castles[Random.Range(0, castles.Length)];
    }

    public void SetWeights()
    {
        foreach (var tile in tiles)
        {
            switch ((TileTypes)tile.autoTileId)
            {
                case TileTypes.Empty:
                case TileTypes.Mountains:
                    tile.weight = -1;
                    break;

                case TileTypes.Tree:
                    tile.weight = 3;
                    break;

                case TileTypes.Hills:
                    tile.weight = 5;
                    break;

                case TileTypes.Monster:
                    tile.weight = 10;
                    break;

                default:
                    tile.weight = 1;
                    break;
            }
        }
    }
}