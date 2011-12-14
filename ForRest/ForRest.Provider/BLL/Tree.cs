// -----------------------------------------------------------------------
// <copyright file="Tree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Tree<T> : ITree<T>
    {
        public abstract void Clear();
        public abstract List<int> Contains(T data);
        public abstract void Add(T data);
        public abstract bool Remove(T data);
        public abstract Node<T> Root { get; set; }
    }
}