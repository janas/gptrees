﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Provider.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for providing containers for trees as well as methods handling tree libraries.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    using ForRest.Provider.BLL;
    using ForRest.Provider.DAL;

    /// <summary>
    /// Class responsible for providing containers for trees as well as methods handling tree libraries.
    /// </summary>
    public class Provider
    {
        #region Constants and Fields

        /// <summary>
        ///   The batch numeric data.
        /// </summary>
        public List<List<double>> BatchNumericData = new List<List<double>>();

        /// <summary>
        ///   The batch performance set.
        /// </summary>
        public List<PerformanceSet> BatchPerformanceSet = new List<PerformanceSet>();

        /// <summary>
        ///   The batch text data.
        /// </summary>
        public List<List<string>> BatchTextData = new List<List<string>>();

        /// <summary>
        ///   The batch tree object.
        /// </summary>
        public List<TreeObject> BatchTreeObject = new List<TreeObject>();

        /// <summary>
        ///   The numeric data.
        /// </summary>
        public List<double> NumericData = new List<double>();

        /// <summary>
        ///   The performance sets.
        /// </summary>
        public List<PerformanceSet> PerformanceSets = new List<PerformanceSet>();

        /// <summary>
        ///   The text data.
        /// </summary>
        public List<string> TextData = new List<string>();

        /// <summary>
        ///   The tree objects.
        /// </summary>
        public List<TreeObject> TreeObjects = new List<TreeObject>();

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets PluginList.
        /// </summary>
        public List<ITreeFactory> PluginList { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The check directory exists.
        /// </summary>
        /// <param name="applicationPath">
        /// The application path. 
        /// </param>
        public void CheckDirectoryExists(string applicationPath)
        {
            if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(applicationPath), "Plugins")))
            {
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(applicationPath), "Plugins"));
            }
        }

        /// <summary>
        /// The create plugin list.
        /// </summary>
        /// <param name="applicationPath">
        /// The application path. 
        /// </param>
        public void CreatePluginList(string applicationPath)
        {
            string folder = Path.Combine(Path.GetDirectoryName(applicationPath), "Plugins");
            this.PluginList = this.GetPlugins<ITreeFactory>(folder);
        }

        /// <summary>
        /// The get plugin description.
        /// </summary>
        /// <param name="applicationPath">
        /// The application path. 
        /// </param>
        /// <returns>
        /// Returns list of string arrays of size 3 containg plugin description. 
        /// </returns>
        public List<string[]> GetPluginDescription(string applicationPath)
        {
            this.CreatePluginList(applicationPath);
            var itemsList = new List<string[]>();
            foreach (ITreeFactory tree in this.PluginList)
            {
                var entry = new string[3];
                string name = tree.GetPluginName();
                string description = tree.GetPluginDescription();
                string advancedInfo = tree.GetType().FullName;
                entry[0] = name;
                entry[1] = description;
                entry[2] = advancedInfo;
                itemsList.Add(entry);
            }

            return itemsList;
        }

        /// <summary>
        /// Method for getting generic puglins list.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="pluginsFolder">
        /// Path to the folder containg libraries (*.dll). 
        /// </param>
        /// <returns>
        /// List of type T. 
        /// </returns>
        public List<T> GetPlugins<T>(string pluginsFolder)
        {
            string[] files = Directory.GetFiles(pluginsFolder, "*.dll");
            var genericList = new List<T>();
            Debug.Assert(typeof(T).IsInterface);
            foreach (string file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!type.IsClass || type.IsNotPublic)
                        {
                            continue;
                        }

                        Type[] interfaces = type.GetInterfaces();
                        if (((IList)interfaces).Contains(typeof(ITreeFactory)))
                        {
                            object obj = (T)Activator.CreateInstance(type);
                            var t = (T)obj;
                            genericList.Add(t);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return genericList;
        }

        /// <summary>
        /// Method for reading a CSV file containg numeric data (all types). Returns list of doubles.
        /// </summary>
        /// <param name="filePath">
        /// Path to file to be parsed. Type of string. 
        /// </param>
        /// <param name="separator">
        /// Character used as separator. Type of char. 
        /// </param>
        /// <returns>
        /// List containg all entries from CSV file converted to double. 
        /// </returns>
        public List<double> LoadNumericData(string filePath, char separator)
        {
            var numericData = new List<double>();
            var dataProvider = new DataProvider();
            var loadNumDataThread =
                new Thread(delegate() { numericData = dataProvider.ParseNumericFile(filePath, separator); });
            loadNumDataThread.Start();
            loadNumDataThread.Join();
            return numericData;
        }

        /// <summary>
        /// Method for reading a CSV file containg text data. Returns list of strings.
        /// </summary>
        /// <param name="filePath">
        /// Path to file to be parsed. Type of string. 
        /// </param>
        /// <param name="separator">
        /// Character used as separator. Type of char. 
        /// </param>
        /// <returns>
        /// List containg all entries from CSV file as strings. 
        /// </returns>
        public List<string> LoadTextData(string filePath, char separator)
        {
            var textData = new List<string>();
            var dataProvider = new DataProvider();
            var loadTextDataThread = new Thread(delegate() { textData = dataProvider.ParseFile(filePath, separator); });
            loadTextDataThread.Start();
            loadTextDataThread.Join();
            return textData;
        }

        /// <summary>
        /// Method for writing results from PerformanceSet list to CSV generic file (comma separated).
        /// </summary>
        /// <param name="performanceSets">
        /// List of type PerformanceSet containing results. 
        /// </param>
        /// <param name="filePath">
        /// Path to where the file will be written. 
        /// </param>
        public void WriteResults(List<PerformanceSet> performanceSets, string filePath)
        {
            var dataProvider = new DataProvider();
            var writeDataThread = new Thread(() => dataProvider.WriteResults(performanceSets, filePath));
            writeDataThread.Start();
            writeDataThread.Join();
        }

        #endregion
    }
}