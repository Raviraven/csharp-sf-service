using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfservice.API.Helpers
{
    public static class CsvHelper
    {
        public static List<T> ReadRecordsFromCSVFile<T, M>(string location)
            where M : ClassMap<T>
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader, new System.Globalization.CultureInfo("pl-PL")))
                {
                    csv.Configuration.RegisterClassMap<M>();
                    var records = csv.GetRecords<T>().ToList();
                    return records;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
