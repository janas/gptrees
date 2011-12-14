// -----------------------------------------------------------------------
// <copyright file="ITree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace ForRest.Provider.BLL
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ITree<T>
    {
        void Clear();
        List<int> Contains(T data);
        void Add(T data);
        bool Remove(T data);
        Node<T> Root { get; set; }
    }
}
