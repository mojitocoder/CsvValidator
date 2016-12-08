using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;

namespace CsvValidator
{
    public class Validation
    {
        private string fileName;

        public Validation(string fileName)
        {
            this.fileName = fileName;
        }

        public void Validate()
        {
            var separator = DetectDelimiter();
            using (var file = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(file))
                {
                    //good file
                    var fileWriter = new StreamWriter(@"C:\Temp\GoodFile.txt");
                    var goodCsv = new CsvWriter(fileWriter);

                    csv.Configuration.TrimFields = true;
                    csv.Configuration.SkipEmptyRecords = true;
                    csv.Configuration.DetectColumnCountChanges = true;

                    csv.Configuration.Delimiter = separator;
                    csv.ReadHeader();
                    var headers = csv.FieldHeaders;
                    var columnCount = headers.Count();

                    while (true)
                    {
                        try
                        {
                            var result = csv.Read();
                            if (!result) break;

                            //valid row => write to good file

                            foreach (var item in csv.CurrentRecord)
                            {
                                goodCsv.WriteField(item);
                            }
                            goodCsv.NextRecord();


                        }
                        catch (Exception ex)
                        {
                                
                                                    
                            //bad rows go here

                            //throw;
                        }
                    }

                    fileWriter.Close();
                    fileWriter.Dispose();
                }
            }
        }

        public string DetectDelimiter()
        {
            var delimiters = new List<string> { ",", "\t" };
            using (var streamReader = new StreamReader(fileName))
            {
                var header = streamReader.ReadLine();
                var counts = delimiters.ToDictionary(key => key, value => header.Count(c => c == Convert.ToChar(value)));
                var delimiter = counts.First(x => x.Value == counts.Values.Max()).Key;
                return delimiter;
            }
        }
    }
}
