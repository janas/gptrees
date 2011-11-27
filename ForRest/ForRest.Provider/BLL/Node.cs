// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Node<T>
    {
        public T Value { get; set; }
        protected NodeList<T> Neighbors { get; set; }
        
        public Node()
        {
            Neighbors = null;
        }

        public Node(T data)
        {
            Value = data;
            Neighbors = null;
        }

        public Node(T data, NodeList<T> neighbors)
        {
            Value = data;
            Neighbors = neighbors;
        }
    }
}
