using UnityEngine;
using UnityEngine.Tilemaps;

public class Practice : MonoBehaviour
{
    //Tile

    public void UpdateAutoTileId()
    {
        //TLRB
        autoFowId = 0;
        for (int i = 0; i < adjacents.Length; ++i)
        {
            if (adjacents[i] == null || !adjacents[i].isVisited)
            {
                autoFowId |= 1 << i;
            }
        }
    }

    public void SpriteTile(int tileId)
    {
        var tile = map.tiles[tileId];
        var tileGo = tileObjs[tileId];
        var ren = tileGo.GetComponent<SpriteRenderer>();

        if (tile.isVisited)

        {
            if (tile.autoTileId != (int)TileTypes.Empty)
            {
                ren.sprite = islandSprites[tile.autoTileId];
            }
            else
            {
                ren.sprite = null;
            }
        }
        else
        {
            ren.sprite = fowSprite[tile.autoFowId]
        }

    }

    public void OnTileVisited(int tileId, int range)
    {
        int centerX = tileId % mapWidth;
        int centerY = tileId / mapWidth;

        int radius = visitRadius;


        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                int x = centerX + j;
                int y = centerY + i;
                if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                {
                    continue;
                }

                int id = y * mapWidth + x;
                map.tiles[id].isVisited = true;
                DecorateTile(id);
            }
        }

        radius += 1;
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                if (i == radius || i == -radius || j == radius || j == -radius)
                {
                    int x = centerX + j;
                    int y = centerY + i;
                    if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                    {
                        continue;
                    }

                    int id = y * mapWidth + x;
                    map.tiles[id].UpdateAutoFowId();
                    DecorateTile(id);
                }
            }
        }
    }
}
