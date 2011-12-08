// -----------------------------------------------------------------------
// <copyright file="Provider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using ForRest.Provider.BLL;
using ForRest.Provider.DAL;

namespace ForRest.Provider
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Provider
    {
        public List<PerformanceSet> PerformanceSets = new List<PerformanceSet>();
        public List<ITree> TextTrees = new List<ITree>();
        public List<ITree> NumericTrees = new List<ITree>();
        public List<string> TextData = new List<string>();
        public List<double> NumericData = new List<double>(); 

        /// <summary>
        /// Method for reading a CSV file containg text data. Returns list of strings.
        /// </summary>
        /// <param name="filePath">Path to file to be parsed. Type of string.</param>
        /// <param name="separator">Character used as separator. Type of char.</param>
        /// <returns>List containg all entries from CSV file as strings.</returns>
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
        /// Method for reading a CSV file containg numeric data (all types). Returns list of doubles.
        /// </summary>
        /// <param name="filePath">Path to file to be parsed. Type of string.</param>
        /// <param name="separator">Character used as separator. Type of char.</param>
        /// <returns>List containg all entries from CSV file converted to double.</returns>
        public List<double> LoadNumericData(string filePath, char separator)
        {
            var numericData = new List<double>();
            var dataProvider = new DataProvider();
            var loadNumDataThread = new Thread(delegate()
                                                      {
                                                          numericData = dataProvider.ParseNumericFile(filePath,
                                                                                                      separator);
                                                      });
            loadNumDataThread.Start();
            loadNumDataThread.Join();
            return numericData;
        }

        /// <summary>
        /// Method for writing results from PerformanceSet list to CSV generic file (comma separated).
        /// </summary>
        /// <param name="performanceSets">List of type PerformanceSet containing results.</param>
        /// <param name="filePath">Path to where the file will be written.</param>
        public void WriteResults(List<PerformanceSet> performanceSets, string filePath)
        {
            var dataProvider = new DataProvider();
            var writeDataThread = new Thread(() => dataProvider.WriteResults(performanceSets, filePath));
            writeDataThread.Start();
            writeDataThread.Join();
        }

        /*public List<ITree<TNode>> GetPlugins<TNode>(string pluginsFolder)
        {
            string[] files = Directory.GetFiles(pluginsFolder, "*.dll");
            var genericList = new List<ITree<TNode>>();
            Debug.Assert(typeof(ITree<TNode>).IsInterface);
            foreach (string file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        if(!type.IsClass || type.IsNotPublic) continue;
                        Type[] interfaces = type.GetInterfaces();
                        var openGeneric = typeof(ITree<TNode>).GetGenericTypeDefinition();
                        if (interfaces.Any(i => typeof(ITree<TNode>).GetGenericTypeDefinition().IsAssignableFrom(i.GetGenericTypeDefinition())))
                        {
                            object obj = (ITree<TNode>)Activator.CreateInstance(type.MakeGenericType(typeof(TNode)));
                            var t = (ITree<TNode>) obj;
                            genericList.Add(t);
                        }
                    }
                }
                catch(Exception e)
                {
                    e.ToString();
                }
            }
            return genericList;
        }
        
        public List<string> CreateItemsList<T>(string applicationPatch)
        {
            string folder = Path.Combine(Path.GetDirectoryName(applicationPatch), "Plugins");
            List<ITree<T>> pluginList = GetPlugins<T>(folder);
            List<string> itemsList = new List<string>();
            foreach (ITree<T> tree in pluginList)
            {
                string name = tree.GetPluginName();
                string description = tree.GetPluginDescription();
                string item = string.Format("{0}, {1}, {2}", tree.GetType().FullName, name, description);
                itemsList.Add(item);
            }
            return itemsList;
        }*/

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
                        if (!type.IsClass || type.IsNotPublic) continue;
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
                    throw;
                }
            }
            return genericList;
        }

        public List<string[]> CreateItemsList<T>(string applicationPatch)
        {
            string folder = Path.Combine(Path.GetDirectoryName(applicationPatch), "Plugins");
            var pluginList = GetPlugins<ITreeFactory>(folder);
            var itemsList = new List<string[]>();
            var entry = new string[3];
            foreach (ITreeFactory tree in pluginList)
            {
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
     }
}
