// -----------------------------------------------------------------------
// <copyright file="ITreeFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ForRest.Provider.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ITreeFactory : ITree
    {
        string GetPluginName();
        string GetPluginDescription();
        ITree GetTree(Type type);
    }
}
