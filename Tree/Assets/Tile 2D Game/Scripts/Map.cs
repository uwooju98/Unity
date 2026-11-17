using System.Linq;
using UnityEngine; // 추가

public enum TileTypes
{
    Empty = -1,

    // 0 ~ 14 해안선
    // 15 땅
    Grass = 15,
    Tree = 16,
    Hills, //언덕
    Mountains, //산
    Towns, // 마을
    Castle, //성 1개만
    Monster, //던전
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

    public bool CreateTree(float erodePercent,
        float erodeIterations,
        float TreePercent)
    {
        DecorateTiles(
            GrassTiles, TreePercent, TileTypes.Tree);

        for (int i = 0; i < erodeIterations; ++i)
        {
            DecorateTiles(
                CoastTiles, erodePercent, TileTypes.Empty);
        }

        return true;
    }

    // 추가: 섬 생성
    public bool CreateIsland(
        float erodePercent,
        float erodeIterations,
        float lakePercent)
    {
        // 육지 내부에 호수 생성
        DecorateTiles(
            LandTiles, lakePercent, TileTypes.Empty);

        // 해안선 침식
        for (int i = 0; i < erodeIterations; ++i)
        {
            DecorateTiles(
                CoastTiles, erodePercent, TileTypes.Empty);
        }

        return true;
    }

    // 추가: 타일 타입 변경
    public void DecorateTiles(
        Tile[] tile, float percent, TileTypes tileType)
    {
        int total = Mathf.FloorToInt(tile.Length * percent);

        ShuffleTiles(tile);

        for (int i = 0; i < total; ++i)
        {
            if (tileType == TileTypes.Empty)
            {
                tile[i].ClearAdjacents();
            }

            tile[i].autoTileId = (int)tileType;
        }
    }

    // 추가: 피셔-예이츠 셔플 알고리즘
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
}