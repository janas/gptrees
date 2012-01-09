// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataProvider.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   Class responsible for data files manipulation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest.Provider.DAL
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using BLL;

    /// <summary>
    /// Class responsible for data files manipulation.
    /// </summary>
    public class DataProvider
    {
        /// <summary>
        /// The parse file.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="separator">
        /// The separator.
        /// </param>
        /// <returns>
        /// List of strings containg the contents of the readed file.
        /// </returns>
        public List<string> ParseFile(string filePath, char separator)
        {
            var strReader = new StreamReader(filePath);
            var cellBuilder = new StringBuilder();
            var readedText = new List<string>();
            int cellCount = 0;
            var rowCellCount = new List<int>();
            bool specialCharacters = false;
            int i = 0;
            string line = strReader.ReadToEnd();
            while (i < line.Length)
            {
                if (!line[i].Equals(separator) && !line[i].Equals('\n') && !line[i].Equals('\r') && !line[i].Equals('"'))
                {
                    cellBuilder.Append(line[i].ToString());
                }

                if (line[i].Equals('\n'))
                {
                    readedText.Add(cellBuilder.ToString());
                    cellBuilder = new StringBuilder();
                    specialCharacters = false;
                    cellCount++;
                    rowCellCount.Add(cellCount);
                    cellCount = 0;
                    i++;
                    continue;
                }

                if (line[i].Equals('\r'))
                {
                    i++;
                    continue;
                }

                if (line[i].Equals(separator) && specialCharacters == false)
                {
                    readedText.Add(cellBuilder.ToString());
                    cellBuilder = new StringBuilder();
                    cellCount++;
                }

                if (line[i].Equals('"') && specialCharacters == false)
                {
                    specialCharacters = true;
                    i++;
                    continue;
                }

                if (line[i].Equals('"') && specialCharacters)
                {
                    specialCharacters = false;
                    i++;
                    continue;
                }

                if (i == line.Length - 1)
                {
                    readedText.Add(cellBuilder.ToString());
                    cellBuilder = new StringBuilder();
                    specialCharacters = false;
                    cellCount++;
                    rowCellCount.Add(cellCount);
                    cellCount = 0;
                }

                i++;
            }

            strReader.Close();
            if (this.CheckCorrectness(rowCellCount))
            {
                return readedText;
            }

            readedText.Clear();
            return readedText;
        }

        /// <summary>
        /// The parse numeric file.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="separator">
        /// The separator.
        /// </param>
        /// <returns>
        /// List of doubles containg the contents of the readed file.
        /// </returns>
        public List<double> ParseNumericFile(string filePath, char separator)
        {
            var numericData = new List<double>();
            List<string> rawData = this.ParseFile(filePath, separator);
            if (rawData != null && rawData.Count > 0)
            {
                foreach (string node in rawData)
                {
                    double numericValue;
                    if (double.TryParse(node, NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                    {
                        numericData.Add(numericValue);
                    }
                }

                return numericData;
            }

            numericData.Clear();
            return numericData;
        }

        /// <summary>
        /// The write results.
        /// </summary>
        /// <param name="perfSet">
        /// The perf set.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        public void WriteResults(List<PerformanceSet> perfSet, string filePath)
        {
            List<string> resultsSet = this.PrepareResults(perfSet);
            var fs = new FileStream(filePath, FileMode.Create);
            var streamWriter = new StreamWriter(fs);
            foreach (string result in resultsSet)
            {
                streamWriter.WriteLine(result);
            }

            streamWriter.Close();
        }

        /// <summary>
        /// The prepare results.
        /// </summary>
        /// <param name="perfSet">
        /// The perf set.
        /// </param>
        /// <returns>
        /// List of strings containing results in format: TreeName, TypeOfTree, NoOfNodes, TypeOfNodes, SearchTime.
        /// </returns>
        private List<string> PrepareResults(List<PerformanceSet> perfSet)
        {
            return
                perfSet.Select(
                    performanceSet =>
                    performanceSet.TreeName + "," + performanceSet.TypeOfTree + "," + performanceSet.NoOfNodes + "," +
                    performanceSet.TypeOfNodes + "," + performanceSet.SearchTime.ToString()).ToList();
        }

        /// <summary>
        /// The check correctness.
        /// </summary>
        /// <param name="rowCellCount">
        /// The row cell count.
        /// </param>
        /// <returns>
        /// True if file is correct CSV file, false otherwise.
        /// </returns>
        private bool CheckCorrectness(List<int> rowCellCount)
        {
            int temp = rowCellCount[0];
            if (rowCellCount.Count == 1)
            {
                return true;
            }

            return rowCellCount.All(i => i == temp);
        }
    }
}