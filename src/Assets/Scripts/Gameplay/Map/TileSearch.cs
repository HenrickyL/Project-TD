using Perikan.AI;
using Perikan.CustomCollections;
using System.Collections.Generic;

namespace Perikan.Gameplay.Map { 
    public static class TileSearch
    {
        //public static IEnumerator FindPathsEnumerator(GameTile[] tiles)
        //{
        //    PriorityQueue<Node<GameTile>> searchFrontier = new();
        //    List<Node<GameTile>> searchExplored = new();
        //    foreach (GameTile t in tiles)
        //    {
        //        t.ClearPath();
        //    }
        //    GameTile initial = tiles[0];
        //    initial.BecomeDestination();
        //    GameTile goal = tiles[tiles.Length -2];

        //    searchFrontier.Enqueue(new Node<GameTile>(initial, 0), 0);

        //    int cost = 0;
        //    while (searchFrontier.Count > 0 && cost < 2000)
        //    {
        //        cost++;
        //        Node<GameTile> tile = searchFrontier.Dequeue();
        //        searchExplored.Add(tile);

        //        if (tile.State == goal) {
        //            yield break;
        //        }


        //        tile.State.ShowPath();
        //        tile.State.SetEnableArrow(true);
        //        yield return null;
        //        yield return null;

        //        foreach (GameTile neighbor in tile.State.Neighbors)
        //        {
        //            if (neighbor != null)
        //            {
        //                GameTile childTile = tile.State.GrowPathTo(neighbor);
        //                float fCost =  cost+childTile.DistanceTo(goal);
        //                Debug.Log($"{cost}: {fCost}");
        //                Node<GameTile> children = new Node<GameTile>(childTile, fCost, tile);
        //                if (children != null &&
        //                    (!ContainsInExploreds(searchExplored, children) || !ContainsInFrontier(searchFrontier, children))) { 
        //                    searchFrontier.Enqueue(children, children.Value);
        //                }else 
        //                    searchFrontier.TryReplace(children, children.Value);
        //            }
        //        }
        //    }
        //    yield break;
        //}


        public static bool ExistDetination(GameTile[] tiles) {
            List<GameTile> destinations = FindAllDestinations(tiles);
            return destinations.Count > 0;
        }

        /// TODO: Verify if path is correct
        private static List<GameTile> FindAllDestinations(GameTile[] tiles) {
            List<GameTile> gameTiles = new();
            foreach (GameTile tile in tiles)
            {
                if (tile == null) continue;
                if (tile.Content.Type == GameTileContentType.Destination)
                {
                    tile.BecomeDestination();
                    gameTiles.Add(tile);
                }
                else
                {
                    tile.ClearPath();
                }
            }
            return gameTiles;
        }


        public static bool FindPath(GameTile[] tiles, bool onVisible = false)
        {
            Queue<GameTile> searchFrontier = new();
            List<GameTile> searchExplored = new();
            //foreach (GameTile tile in tiles)
            //{
            //    if (tile.Content.Type == GameTileContentType.Destination)
            //    {
            //        tile.BecomeDestination();
            //        searchFrontier.Enqueue(tile);
            //    }
            //    else { 
            //        tile.ClearPath();
            //    }
            //}

            //if (searchFrontier.Count == 0) yield return false;


            List<GameTile> destinations = FindAllDestinations(tiles);
            if (destinations.Count == 0) return false;
            foreach (GameTile tile in destinations)
            {
                searchFrontier.Enqueue(tile);
            }

            //GameTile initial = tiles[tiles.Length/2];
            //initial.BecomeDestination();
            //GameTile goal = tiles.Last();//[tiles.Length - 2];

            //searchFrontier.Enqueue(initial);
            //searchFrontier.Enqueue(new Node<GameTile>(initial, 0), 0);

            int cost = 0;
            while (searchFrontier.Count > 0 && cost < 3500)
            {
                cost++;
                GameTile tile = searchFrontier.Dequeue();
                searchExplored.Add(tile);

            
                //if (!(searchFrontier.Count > 0))
                //{
                //    yield return true;
                //}

                foreach (Direction dir in tile.NeighborsOrder)
                {
                    GameTile neighbor = tile.Neighbors[(int)dir];
                    if (neighbor != null)
                    {
                        if (!searchExplored.Contains(neighbor) && !searchFrontier.Contains(neighbor))
                        {
                            GameTile childTile = tile.GrowPathTo(neighbor, dir);
                            if(childTile != null)
                                searchFrontier.Enqueue(childTile);
                        }
                        //else
                        //    searchFrontier.TryReplace(children, children.Value);
                    }
                }
            }

            if (onVisible) { 
                foreach (GameTile tile in tiles)
                {
                    if (tile.isEmpty && !tile.HasPath)
                    {
                        //return false;
                        //tile.Content.Content.Disable();
                    }
                    else
                    {
                        tile.ShowPath();
                        //tile.SetEnableArrow(true);
                    }
                }
            }
            return true;
        }

        //public static IEnumerator<bool> FindPathsEnumerator(GameTile[] tiles)
        //{
        //    Queue<GameTile> searchFrontier = new();
        //    List<GameTile> searchExplored = new();
        //    //foreach (GameTile tile in tiles)
        //    //{
        //    //    if (tile.Content.Type == GameTileContentType.Destination)
        //    //    {
        //    //        tile.BecomeDestination();
        //    //        searchFrontier.Enqueue(tile);
        //    //    }
        //    //    else { 
        //    //        tile.ClearPath();
        //    //    }
        //    //}

        //    //if (searchFrontier.Count == 0) yield return false;


        //    List<GameTile> destinations = FindAllDestinations(tiles);
        //    foreach (GameTile tile in destinations)
        //    {
        //        searchFrontier.Enqueue(tile);
        //    }


        //    //GameTile initial = tiles[tiles.Length/2];
        //    //initial.BecomeDestination();
        //    //GameTile goal = tiles.Last();//[tiles.Length - 2];

        //    //searchFrontier.Enqueue(initial);
        //    //searchFrontier.Enqueue(new Node<GameTile>(initial, 0), 0);


        //    while (searchFrontier.Count > 0)
        //    {
        //        GameTile tile = searchFrontier.Dequeue();
        //        searchExplored.Add(tile);

        //        tile.ShowPath();
        //        tile.SetEnableArrow(true);
        //        yield return false;

        //        //if (!(searchFrontier.Count > 0))
        //        //{
        //        //    yield return true;
        //        //}
            
        //        yield return false;

        //        foreach (GameTile neighbor in tile.Neighbors)
        //        {
        //            if (neighbor != null)
        //            {
        //                GameTile childTile = tile.GrowPathTo(neighbor);
        //                if (childTile != null && 
        //                    (!searchExplored.Contains(childTile) && !searchFrontier.Contains(childTile)))
        //                {
        //                    //searchFrontier.Enqueue(children, children.Value);
        //                    searchFrontier.Enqueue(childTile);
        //                }
        //                //else
        //                //    searchFrontier.TryReplace(children, children.Value);
        //            }
        //        }
        //    }
        //    yield return true;
        //}

        private static bool ContainsInExploreds(List<Node<GameTile>> searchExplored, Node<GameTile> item) {
            foreach (var node in searchExplored)
            {
                if (node.State == item.State)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsInFrontier(PriorityQueue<Node<GameTile>> searchFrontier, Node<GameTile> item)
        {
            foreach (var (state, priority) in searchFrontier)
            {
                if (state.State == item.State && priority == item.Value)
                {
                    return true;
                }
            }

            return false;
        }

        //public static void FindPaths(GameTile[] tiles)
        //{
        //    Queue<GameTile> searchFrontier = new();
        //    List<GameTile> searchExplored = new();
        //    foreach (GameTile t in tiles)
        //    {
        //        t.ClearPath();
        //    }
        //    tiles[0].BecomeDestination();
        //    searchFrontier.Enqueue(tiles[0]);
        //    int count = 0;
        //    while (searchFrontier.Count > 0 && count < 1000)
        //    {
        //        count++;
        //        GameTile tile = searchFrontier.Dequeue();
        //        searchExplored.Add(tile);
        //        tile.ShowPath();
        //        tile.SetEnableArrow(true);

        //        foreach (GameTile neighbor in tile.Neighbors)
        //        {
        //            if (neighbor != null)
        //            {
        //                GameTile children = tile.GrowPathTo(neighbor);
        //                if (children != null && !searchFrontier.Contains(children) && !searchExplored.Contains(children))
        //                    searchFrontier.Enqueue(children);
        //                //searchFrontier.Push(neighbor);
        //            }
        //        }
        //    }
        //}


        //public static IEnumerator FindPath(Perikan.AI.Node<GameTile> root)
        //{
        //    Perikan.AI.Node<GameTile> node = root;
        //    while (node.Father != null) {
        //        GameTile tile = node.State;
        //        GameTile father = node.Father.State;
        //        tile.GrowPathTo(father);
        //        tile.ShowPath();
        //        tile.SetEnableArrow(true);
        //        yield return null;
        //        node = node.Father;
        //    }
        //    yield break;
        //}
    }
}