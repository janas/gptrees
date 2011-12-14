// -----------------------------------------------------------------------
// <copyright file="ITreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ITreeFactory
    {
        string Name { get; }
        string GetPluginName();
        string GetPluginDescription();
        ITree<T> GetTree<T>();
        ITree<T> GetTree<T>(int degree);
    }
}
