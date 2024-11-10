using System.Collections.Generic;
public static class TileSearch
{
    private static Queue<GameTile> searchFrontier = new ();
    private static List<GameTile> searchExplored = new();


    public static void FindPaths(GameTile[] tiles)
    {
        foreach (GameTile t in tiles)
        {
            t.ClearPath();
        }
        tiles[0].BecomeDestination();
        searchFrontier.Enqueue(tiles[0]);


        while (searchFrontier.Count > 0)
        {
            GameTile tile = searchFrontier.Dequeue();
            searchExplored.Add(tile);

            for (int dir = (int)Direction.North; dir < (int)Direction.West; dir++)
            {
                GameTile neighbor = tile.Neighbors[dir];
                if (neighbor != null && !searchExplored.Contains(neighbor)) {
                    neighbor.SetEnableArrow(true);
                    searchFrontier.Enqueue(tile.GrowPathTo(neighbor));
                }
            }
        }

        foreach (GameTile tile in tiles)
        {
            tile.ShowPath();
        }
    }

}
