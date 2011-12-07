// -----------------------------------------------------------------------
// <copyright file="ITree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ForRest.Provider.BLL
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ITree<T>
    {
        void Clear();
        bool Contains(T data);
        void Add(T data);
        bool Remove(T data);
    }

    public interface ITree
    {
        
    }
}
