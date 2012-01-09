// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITreeFactory.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Interace responsible for managing trees.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// Interace responsible for managing trees.
    /// </summary>
    public interface ITreeFactory
    {
        /// <summary>
        /// Gets Name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether NeedDegree.
        /// </summary>
        bool NeedDegree { get; }

        /// <summary>
        /// The get plugin name.
        /// </summary>
        /// <returns>
        /// Returns plugin name.
        /// </returns>
        string GetPluginName();

        /// <summary>
        /// The get plugin description.
        /// </summary>
        /// <returns>
        /// Returns plugin description.
        /// </returns>
        string GetPluginDescription();

        /// <summary>
        /// Creates instance of a given tree.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        ITree<T> GetTree<T>();

        /// <summary>
        /// Creates instance of a given tree with specific degree.
        /// </summary>
        /// <param name="degree">
        /// The degree of the tree.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        ITree<T> GetTree<T>(int degree);
    }
}