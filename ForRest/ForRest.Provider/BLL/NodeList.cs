// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeList.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class NodeList<T> : Collection<Node<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeList{T}"/> class.
        /// </summary>
        /// <param name="initialSize">
        /// The initial size.
        /// </param>
        public NodeList(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                Items.Add(default(Node<T>));
            }
        }

        /// <summary>
        /// The find by value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// </returns>
        public Node<T> FindByValue(T value)
        {
            foreach (var node in Items)
            {
                if (node.Values.Contains(value))
                    return node;
            }

            return null;
        }
    }
}