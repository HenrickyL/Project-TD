using System.Collections.Generic;
using System.Collections;
public static class TileSearch
{
    public static IEnumerator FindPaths(GameTile[] tiles)
    {
        Stack<GameTile> searchFrontier = new();
        List<GameTile> searchExplored = new();
        
        foreach (GameTile t in tiles)
        {
            t.ClearPath();
        }
        tiles[0].BecomeDestination();
        searchFrontier.Push(tiles[0]);


        while (searchFrontier.Count > 0)
        {
            GameTile tile = searchFrontier.Pop();
            searchExplored.Add(tile);
            //tile.ShowPath();
            //tile.SetEnableArrow(true);

            foreach (GameTile neighbor in tile.Neighbors)
            {
                if (neighbor != null) {
                    GameTile children = tile.GrowPathTo(neighbor);
                    if(children!= null && !searchExplored.Contains(children))
                        searchFrontier.Push(children);
                    //searchFrontier.Push(neighbor);
                }
            }
        }
        yield return null;

        //foreach (GameTile tile in tiles)
        //{
        //    tile.ShowPath();
        //}
        yield return null;
    }

}
