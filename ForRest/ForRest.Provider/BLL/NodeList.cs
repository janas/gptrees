// -----------------------------------------------------------------------
// <copyright file="NodeList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.ObjectModel;

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NodeList<T> : Collection<Node<T>> 
    { 
        public NodeList(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                Items.Add(default(Node<T>));
            }
        }

        public Node<T> FindByValue(T value)
        {
            foreach (Node<T> node in Items)
            {
                if (node.Values.Contains(value))
                    return node;
            }
            return null;
        }
    }
}
