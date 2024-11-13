using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Perikan.AI;

using UnityEngine;
public static class TileSearch
{
    

    public static IEnumerator FindPathsEnumerator(GameTile[] tiles)
    {
        CustomCollections.PriorityQueue<Node<GameTile>> searchFrontier = new();
        List<Node<GameTile>> searchExplored = new();
        foreach (GameTile t in tiles)
        {
            t.ClearPath();
        }
        GameTile initial = tiles[0];
        initial.BecomeDestination();
        GameTile goal = tiles.Last();


        searchFrontier.Enqueue(new Node<GameTile>(initial, 0), 0);

        int cost = 0;
        while (searchFrontier.Count > 0 && cost < 2000)
        {
            cost++;
            Node<GameTile> tile = searchFrontier.Dequeue();
            searchExplored.Add(tile);

            if (tile.State == goal) {
                yield break;
            }


            tile.State.ShowPath();
            tile.State.SetEnableArrow(true);
            yield return null;
            yield return null;

            foreach (GameTile neighbor in tile.State.Neighbors)
            {
                if (neighbor != null)
                {
                    GameTile childTile = tile.State.GrowPathTo(neighbor);
                    float fCost =  childTile.DistanceTo(goal);
                    Debug.Log($"{cost}: {fCost}");
                    Node<GameTile> children = new Node<GameTile>(childTile, fCost, tile);
                    if (children != null &&
                        (!ContainsInExploreds(searchExplored, children) || !ContainsInFrontier(searchFrontier, children))) { 
                        searchFrontier.Enqueue(children, children.Value);
                    }else 
                        searchFrontier.TryReplace(children, children.Value);
                }
            }
        }
        yield break;
    }

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

    private static bool ContainsInFrontier(CustomCollections.PriorityQueue<Node<GameTile>> searchFrontier, Node<GameTile> item)
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

    public static void FindPaths(GameTile[] tiles)
    {
        Queue<GameTile> searchFrontier = new();
        List<GameTile> searchExplored = new();
        foreach (GameTile t in tiles)
        {
            t.ClearPath();
        }
        tiles[0].BecomeDestination();
        searchFrontier.Enqueue(tiles[0]);
        int count = 0;
        while (searchFrontier.Count > 0 && count < 1000)
        {
            count++;
            GameTile tile = searchFrontier.Dequeue();
            searchExplored.Add(tile);
            tile.ShowPath();
            tile.SetEnableArrow(true);

            foreach (GameTile neighbor in tile.Neighbors)
            {
                if (neighbor != null)
                {
                    GameTile children = tile.GrowPathTo(neighbor);
                    if (children != null && !searchFrontier.Contains(children) && !searchExplored.Contains(children))
                        searchFrontier.Enqueue(children);
                    //searchFrontier.Push(neighbor);
                }
            }
        }
    }


    public static IEnumerator FindPath(Perikan.AI.Node<GameTile> root)
    {
        Perikan.AI.Node<GameTile> node = root;
        while (node.Father != null) {
            GameTile tile = node.State;
            GameTile father = node.Father.State;
            tile.GrowPathTo(father);
            tile.ShowPath();
            tile.SetEnableArrow(true);
            yield return null;
            node = node.Father;
        }
        yield break;
    }

}


