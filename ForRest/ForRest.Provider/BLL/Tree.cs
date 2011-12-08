// -----------------------------------------------------------------------
// <copyright file="Tree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Tree<T> : ITree<T>
    {
        public abstract void Clear();
        public abstract bool Contains(T data);
        public abstract void Add(T data);
        public abstract bool Remove(T data);
    }
}
