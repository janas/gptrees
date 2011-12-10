// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Node<T>
    {
        public List<T> Values { get; set; }
        protected NodeList<T> Neighbors { get; set; }
        
        public Node()
        {
            Values = null;
            Neighbors = null;
        }

        public Node(List<T> data)
        {
            Values = data;
            Neighbors = null;
        }

        public Node(List<T> data, NodeList<T> neighbors)
        {
            Values = data;
            Neighbors = neighbors;
        }

        public Node<T>[] GetNeighborsArray()
        {
            Node<T>[] result = new Node<T>[Neighbors.Count];
            Neighbors.CopyTo(result, 0);
            return result;
        }

        public NodeList<T> GetNeighborsList()
        {
            return Neighbors;
        }
    }
}
