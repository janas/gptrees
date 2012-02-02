// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResult.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The search result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    using System.Collections.Generic;

    /// <summary>
    /// The search result.
    /// </summary>
    public struct SearchResult
    {
        #region Constants and Fields

        /// <summary>
        /// The nodes visited.
        /// </summary>
        public int NodesVisited;

        /// <summary>
        /// The search path.
        /// </summary>
        public List<int> SearchPath;

        #endregion
    }
}