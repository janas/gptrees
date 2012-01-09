// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceSet.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class - container for performance results.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// Class - container for performance results.
    /// </summary>
    public class PerformanceSet
    {
        /// <summary>
        /// Gets or sets TreeName.
        /// </summary>
        public string TreeName { get; set; }

        /// <summary>
        /// Gets or sets TypeOfTree.
        /// </summary>
        public string TypeOfTree { get; set; }

        /// <summary>
        /// Gets or sets NoOfNodes.
        /// </summary>
        public string NoOfNodes { get; set; }

        /// <summary>
        /// Gets or sets TypeOfNodes.
        /// </summary>
        public string TypeOfNodes { get; set; }

        /// <summary>
        /// Gets or sets SearchTime.
        /// </summary>
        public string SearchTime { get; set; }
    }
}