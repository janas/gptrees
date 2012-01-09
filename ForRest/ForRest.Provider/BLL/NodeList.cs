// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeList.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for holding nodes children.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Class responsible for holding nodes children.
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
    }
}