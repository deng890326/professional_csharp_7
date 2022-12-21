using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFileReader
{
    internal class DynamicFileHelper
    {
        private StreamReader? openFile(string fileName)
        {
            try
            {
                FileStream fs = File.OpenRead(fileName);
                return new StreamReader(fs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IEnumerable<dynamic> ParseFile(string fileName)
        {
            using (StreamReader? sr = openFile(fileName))
            {
                string? headerLine;
                if (sr == null || (headerLine = sr.ReadLine()) == null)
                    return Enumerable.Empty<dynamic>();

                string[] headers = headerLine.Split(',');
                List<dynamic> res = new List<dynamic>();
                string? dataLine;
                while ((dataLine = sr.ReadLine()) != null)
                {
                    string[] data = dataLine.Split(',');
                    dynamic dyn = new ExpandoObject();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        string header = headers[i].Trim();
                        string? dat = i < data.Length ? data[i].Trim() : null;
                        ((IDictionary<string, object?>)dyn).Add(header, dat);
                        //((IDictionary<string, object?>)dyn)[header] = dat;
                        //dyn.header = dat;
                    }
                    res.Add(dyn);
                }
                return res;
            }
        }
    }
}
