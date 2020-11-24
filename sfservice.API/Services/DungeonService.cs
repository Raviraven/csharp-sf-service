using CsvHelper;
using sfservice.API.Mappers;
using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfservice.API.Services
{
    public class DungeonService : IDungeonService
    {
        public List<Dungeon> GetDungeons()
        {
            string location = "D:\\Projekty\\csharp\\sf-service\\dungeonsPL.csv";
            return readDungeonCSVFile(location);
        }

        private List<Dungeon> readDungeonCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader, new System.Globalization.CultureInfo("pl-PL")))
                {
                    csv.Configuration.RegisterClassMap<DungeonMap>();
                    var records = csv.GetRecords<Dungeon>().ToList();
                    return records;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
