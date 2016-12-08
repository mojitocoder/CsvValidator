using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvValidator
{
    public class XeroxValidator
    {
        private string fileName;

        public XeroxValidator(string fileName)
        {
            this.fileName = fileName;
        }

        public void Validate()
        {
            var dir = Path.GetDirectoryName(fileName);
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);

            var goodFileName = Path.Combine(dir, nameWithoutExtension + "_Valid" + extension);
            var badFileName = Path.Combine(dir, nameWithoutExtension + "_Invalid" + extension);

            char separator = '\t';
            using (var goodFile = File.CreateText(goodFileName))
            {
                using (var badFile = File.CreateText(badFileName))
                {
                    var lines = File.ReadLines(fileName);

                    var headers = lines.First();
                    goodFile.WriteLine(headers);
                    badFile.WriteLine(headers);

                    var headerCount = headers.Count(foo => foo == separator);

                    foreach (var line in lines.Skip(1))
                    {
                        var lineCount = line.Count(foo => foo == separator);
                        if (lineCount == headerCount)
                        {
                            goodFile.WriteLine(line);
                        }
                        else
                        {
                            badFile.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
