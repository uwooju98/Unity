using System.Collections.Generic;

public enum Sides
{
    Top,
    Left,
    Right,
    Bottom,
}

public class Tile
{
    public static readonly int[] tableWeight =
    {
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        5,
        10,
        50,
        1,
        1,
        100,
    };

    public int id;
    public int autoTileId;
    public int fowAutoTileId;

    public Tile[] adjacents = new Tile[4];
    public Tile[] fowAdjacents = new Tile[4];

    public Tile previous;

    public bool isVisited;
    public bool isRevealed;

    public bool CanMove => autoTileId != (int)TileTypes.Empty && Weight < int.MaxValue;

    public int Weight
    {
        get
        {
            if (autoTileId < 0 || autoTileId >= tableWeight.Length)
            {
                return int.MaxValue;
            }
            return tableWeight[autoTileId];
        }
    }

    public void UpdateAutoTileId()
    {
        autoTileId = 0;
        for (int i = 0; i < adjacents.Length; ++i)
        {
            if (adjacents[i] != null)
            {
                autoTileId |= 1 << i;
            }
        }
    }

    public void RemoveAdjacents(Tile tile)
    {
        for (int i = 0; i < adjacents.Length; ++i)
        {
            if (adjacents[i] == null)
                continue;
            if (adjacents[i].id == tile.id)
            {
                adjacents[i] = null;
                break;
            }
        }
        UpdateAutoTileId();
    }

    public void ClearAdjacents()
    {
        for (int i = 0; i < adjacents.Length; ++i)
        {
            if (adjacents[i] == null)
                continue;
            adjacents[i].RemoveAdjacents(this);
            adjacents[i] = null;
        }

        UpdateAutoTileId();
    }

    public void UpdateFowAutoTileId()
    {
        fowAutoTileId = 0;
        for (int i = 0; i < fowAdjacents.Length; ++i)
        {
            if (fowAdjacents[i] != null)
            {
                fowAutoTileId |= 1 << i;
            }
        }
    }

    public void RemoveFowAdjacents(Tile tile)
    {
        for (int i = 0; i < fowAdjacents.Length; ++i)
        {
            if (fowAdjacents[i] == null)
                continue;

            if (fowAdjacents[i].id == tile.id)
            {
                fowAdjacents[i] = null;
                break;
            }
        }

        UpdateFowAutoTileId();
    }

    public List<Tile> ClearFowAdjacents()
    {
        var tiles = new List<Tile>();

        for (int i = 0; i < fowAdjacents.Length; ++i)
        {
            if (fowAdjacents[i] == null)
                continue;

            tiles.Add(fowAdjacents[i]);
            fowAdjacents[i].RemoveFowAdjacents(this);
            fowAdjacents[i] = null;
        }

        UpdateFowAutoTileId();
        return tiles;
    }
}
