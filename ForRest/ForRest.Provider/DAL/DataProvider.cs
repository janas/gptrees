// -----------------------------------------------------------------------
// <copyright file="DataProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ForRest.Provider.BLL;

namespace ForRest.Provider.DAL
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DataProvider
    {
        public List<string> ParseFile(string filePath, char separator)
        {
            var strReader = new StreamReader(filePath);
            var cellBuilder = new StringBuilder();
            var readedText = new List<string>();
            int cellCount = 0;
            int row = 1;
            int[] rowCellCount = new int[] {};
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
                        row++;
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
                    if (line[i].Equals('"') && specialCharacters == true)
                    {
                        specialCharacters = false;
                        i++;
                        continue;
                    }
                    i++;
            }
            strReader.Close();
            return readedText;
        }

        public List<double> ParseNumericFile(string filePath, char separator)
        {
            var numericData = new List<double>();
            List<string> rawData = ParseFile(filePath, separator);
            foreach (var node in rawData)
            {
                double numericValue;
                if (double.TryParse(node, NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
                {
                    numericData.Add(numericValue);
                }
            }
            return numericData;
        }

        public void WriteResults(List<PerformanceSet> perfSet, string filePath)
        {
            List<string> resultsSet = PrepareResults(perfSet);
            var fs = new FileStream(filePath, FileMode.Create);
            var streamWriter = new StreamWriter(fs);
            foreach (var result in resultsSet)
            {
                streamWriter.WriteLine(result);
            }
            streamWriter.Close();
        }
   
        private List<string> PrepareResults(List<PerformanceSet> perfSet)
        {
            return perfSet.Select(performanceSet => performanceSet.TreeName + "," + performanceSet.TypeOfTree + "," + performanceSet.NoOfNodes + "," + performanceSet.TypeOfNodes + "," + performanceSet.SearchTime.ToString()).ToList();
        }
    }
}
