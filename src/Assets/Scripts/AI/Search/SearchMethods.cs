using System;
using System.Collections.Generic;

namespace Perikan.AI
{
    public static class SearchMethods
    {
        public static Node<T> BreadthFirstSearch<T>(T initial, T goal) where T : IState<T>, IEquatable<T>
        {
            Queue<Node<T>> frontier = new();
            List<Node<T>> explored = new();

            Node<T> node = new Node<T>(initial);
            frontier.Enqueue(node);
            int count = 0;

            while (frontier.Count > 0 && count <200) {
                count++;
                node = frontier.Dequeue();

                if (node.State.Equals(goal)) {
                    return node;
                }
                explored.Add(node);

                foreach (T childState in node.Childrens) {
                    if (childState == null) continue;
                    Node<T> child = new Node<T>(childState, node);
                    if (!InExplored(explored, child.State)) {
                        frontier.Enqueue(child);
                    }
                }
            }

            return null;
        }

        private static bool InExplored<T>(List<Node<T>> explored, T state) where T : IState<T>, IEquatable<T>
        {
            foreach (Node<T> node in explored)
            {
                if (node.State.Equals(state))
                    return true;
            }
            return false;
        }
    }
}