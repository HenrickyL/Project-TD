
namespace Perikan.AI { 
    public class Node<T> where T : IState<T>
    {
        public T State{ get; set; }
        public float Value { get; set; }
        public Node<T> Father { get; set; }

        public T[] Childrens => State.GetChilds();

        public Node(T state, float value, Node<T> father = null) { 
            State = state;
            Value = value;
            Father = father;
        }

        public Node(T state, Node<T> father= null)
        {
            State = state;
            Value = 0;
            Father = father;
        }
    }   
}
