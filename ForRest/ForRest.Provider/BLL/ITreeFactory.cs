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
    public interface ITreeFactory
    {
        string GetPluginName();
        string GetPluginDescription();
        ITree<T> GetTree<T>();
    }
}
