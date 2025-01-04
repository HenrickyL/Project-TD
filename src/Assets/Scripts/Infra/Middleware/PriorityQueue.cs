using System;
using System.Collections;
using System.Collections.Generic;

namespace Perikan.CustomCollections
{
    public class PriorityQueue<T> : IEnumerable<(T item, float priority)>
    {
        private List<(T item, float priority)> _heap = new List<(T item, float priority)>();

        public int Count => _heap.Count;

        public void Enqueue(T item, float priority)
        {
            _heap.Add((item, priority));
            HeapifyUp(_heap.Count - 1);
        }

        public T Dequeue()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            T rootItem = _heap[0].item;
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);

            if (_heap.Count > 0)
            {
                HeapifyDown(0);
            }

            return rootItem;
        }
        public bool Contains(T item)
        {
            foreach (var element in _heap)
            {
                if (EqualityComparer<T>.Default.Equals(element.item, item))
                { return true; }
            }
            return false;

        }

        public bool TryReplace(T item, float newPriority)
        {
            for (int i = 0; i < _heap.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_heap[i].item, item))
                {
                    if (_heap[i].priority > newPriority)
                    {
                        _heap[i] = (item, newPriority);
                        HeapifyUp(i);
                        HeapifyDown(i);
                    }
                    return true;
                }
            }
            return false;
        }

        public T Peek()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            return _heap[0].item;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (_heap[index].priority >= _heap[parentIndex].priority)
                {
                    break;
                }

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int smallestIndex = index;

                if (leftChildIndex < _heap.Count && _heap[leftChildIndex].priority < _heap[smallestIndex].priority)
                {
                    smallestIndex = leftChildIndex;
                }

                if (rightChildIndex < _heap.Count && _heap[rightChildIndex].priority < _heap[smallestIndex].priority)
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex == index)
                {
                    break;
                }

                Swap(index, smallestIndex);
                index = smallestIndex;
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }

        public IEnumerator<(T item, float priority)> GetEnumerator()
        {
            return _heap.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

