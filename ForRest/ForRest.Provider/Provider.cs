// -----------------------------------------------------------------------
// <copyright file="Provider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System;
using System.Collections.Generic;
using ForRest.Provider.BLL;
using ForRest.Provider.DAL;

namespace ForRest.Provider
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Provider
    {
        public List<string> LoadTextData(string filePath, char separator)
        {
            DataProvider dataProvider = new DataProvider();
            return dataProvider.ParseFile(filePath, separator);
        }

        public List<double> LoadNumericData(string filePath, char separator)
        {
            DataProvider dataProvider = new DataProvider();
            //to do
            return null;
        }

        public List<T> GetPlugins<T>(string pluginsFolder)
        {
            string[] files = Directory.GetFiles(pluginsFolder, "*.dll");
            List<T> genericList = new List<T>();
            Debug.Assert(typeof(T).IsInterface);
            foreach (string file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        if(!type.IsClass || type.IsNotPublic) continue;
                        Type[] interfaces = type.GetInterfaces();
                        if(((IList)interfaces).Contains(typeof(T)))
                        {
                            object obj = (T)Activator.CreateInstance(type);
                            T t = (T) obj;
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
        
        public List<string> CreateItemsList(string applicationPatch)
        {
            string folder = Path.Combine(Path.GetDirectoryName(applicationPatch), "Plugins");
            List<ITree> pluginList = GetPlugins<ITree>(folder);
            List<string> itemsList = new List<string>();
            foreach (ITree tree in pluginList)
            {
                string name = tree.GetPluginName();
                string description = tree.GetPluginDescription();
                string item = string.Format("{0}, {1}, {2}", tree.GetType().FullName, name, description);
                itemsList.Add(item);
            }
            return itemsList;
        }

        public string test()
        {
            Assembly ass = Assembly.LoadFile(@"c:\ForRest.BST.dll");
            Type[] testtype = ass.GetTypes();
           // object bstIns = Activator.CreateInstance(testtype);
            //PropertyInfo prp = testtype.GetProperty("Add");
            //return prp.ToString();
            string types = null;
            foreach(Type t in testtype)
            {
                if (t.FullName != null) types = t.FullName + '\n';
            }
            //object obj = Activator.CreateInstance(types);
            return types;
        }
    }
}
