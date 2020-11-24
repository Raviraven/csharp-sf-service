using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfservice.API.Services
{
    public class CsvService : ICsvService
    {
        public  List<Type> ReadRecordsFromCSVFile<Type, TypeMapper>(string location)
            where TypeMapper : ClassMap<Type>
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader, new System.Globalization.CultureInfo("pl-PL")))
                {
                    csv.Configuration.RegisterClassMap<TypeMapper>();
                    var records = csv.GetRecords<Type>().ToList();
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
