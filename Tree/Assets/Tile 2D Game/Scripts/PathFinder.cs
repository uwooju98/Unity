using System.Collections.Generic;
using UnityEngine;

public enum SearchType
{
    DFS,
    BFS,
    AStar,
}

internal class PathFinder
{
    public List<Tile> path = new List<Tile>();

    public bool AStar(Tile startTile, Tile endTile, int cols)
    {
        path.Clear();
        startTile.previous = null;

        var findTile = new PriorityQueue<Tile, int>();
        var visited = new HashSet<Tile>();
        var Costs = new Dictionary<Tile, int>();

        Costs.Add(startTile, 0);

        int g = 0;
        int h = ManhattanDistance(startTile, endTile, cols);
        int f = g + h;

        findTile.Enqueue(startTile, f);

        while (findTile.Count > 0)
        {
            var currentTile = findTile.Dequeue();
            
            if (visited.Contains(currentTile))
            {
                continue;
            }

            visited.Add(currentTile);

            if (currentTile == endTile)
            {
                BuildPath(endTile);
                return true;
            }

            foreach (var adjacents in currentTile.adjacents)
            {
                if (adjacents == null ||
                    !adjacents.CanVisit ||
                    visited.Contains(adjacents))
                {
                    continue;
                }

                int newG = Costs[currentTile] + adjacents.weight;

                if (!Costs.TryGetValue(adjacents, out int oldG) || newG < oldG)
                {
                    Costs[adjacents] = newG;
                    adjacents.previous = currentTile;

                    int newH = ManhattanDistance(adjacents, endTile, cols);
                    int newF = newG + newH;

                    findTile.Enqueue(adjacents, newF);
                }
            }
        }

        return false;
    }


    public bool DFS(Tile startTile,Tile endTile)
    {
        path.Clear();
        startTile.previous = null;

        var findTile = new Stack<Tile>();
        var visited = new HashSet<Tile>();

        findTile.Push(startTile);
        visited.Add(startTile);

        while(findTile.Count > 0)
        {
            var currentTile = findTile.Pop();

            if (currentTile == endTile)
            {
                BuildPath(endTile);
                return true;
            }

            foreach (var adjacents in currentTile.adjacents)
            {
                if (adjacents == null || !adjacents.CanVisit || visited.Contains(adjacents))
                {
                    continue;
                }

                adjacents.previous = currentTile;

                findTile.Push(adjacents);
                visited.Add(adjacents);
            }
        }
        return false;
    }

    public bool BFS(Tile startTile, Tile endTile)
    {
        path.Clear();
        startTile.previous = null;

        var findTile = new Queue<Tile>();
        var visited = new HashSet<Tile>();

        findTile.Enqueue(startTile);
        visited.Add(startTile);

        while (findTile.Count > 0)
        {
            var currentTile = findTile.Dequeue();

            if (currentTile == endTile)
            {
                BuildPath(endTile);
                return true;
            }

            foreach (var adjacents in currentTile.adjacents)
            {
                if (adjacents == null ||
                    !adjacents.CanVisit ||
                    visited.Contains(adjacents))
                {
                    continue;
                }

                adjacents.previous = currentTile;

                findTile.Enqueue(adjacents);
                visited.Add(adjacents);
            }
        }

        return false;
    }

    private void BuildPath(Tile endTile)
    {
        Tile step = endTile;

        while (step != null)
        {
            path.Add(step);
            step = step.previous;
        }

        path.Reverse();
    }

    private int ManhattanDistance(Tile current, Tile end, int cols)
    {
        int currentRow = current.id / cols;
        int currentCol = current.id % cols;

        int endRow = end.id / cols;
        int endCol = end.id % cols;

        int rowDistance = Mathf.Abs(currentRow - endRow);
        int colDistance = Mathf.Abs(currentCol - endCol);

        return rowDistance + colDistance;
    }
}

