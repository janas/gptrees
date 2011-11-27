// -----------------------------------------------------------------------
// <copyright file="DataProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Text;
using System.Collections.Generic;

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
                        //rowCellCount[row] = cellCount;
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
    }
}
