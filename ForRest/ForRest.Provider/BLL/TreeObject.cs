// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeObject.cs" company="">
//   
// </copyright>
// <summary>
//   The tree object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    /// <summary>
    /// The tree object.
    /// </summary>
    public class TreeObject
    {
        /// <summary>
        /// The numeric tree.
        /// </summary>
        public readonly ITree<double> NumericTree;

        /// <summary>
        /// The text tree.
        /// </summary>
        public readonly ITree<string> TextTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeObject"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="textTree">
        /// The text tree.
        /// </param>
        public TreeObject(string name, string type, ITree<string> textTree)
        {
            this.Name = name;
            this.Type = type;
            this.TextTree = textTree;
            this.NumericTree = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeObject"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="numericTree">
        /// The numeric tree.
        /// </param>
        public TreeObject(string name, string type, ITree<double> numericTree)
        {
            this.Name = name;
            this.Type = type;
            this.TextTree = null;
            this.NumericTree = numericTree;
        }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public string Type { get; set; }
    }
}