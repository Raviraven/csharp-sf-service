using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using sfservice.API.Mappers;
using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sfservice.API.Helpers;

namespace sfservice.API.Services
{
    public class DungeonService : IDungeonService
    {
        private readonly IConfiguration _config;

        public DungeonService(IConfiguration config)
        {
            _config = config;
        }

        public List<Dungeon> GetDungeons()
        {
            var location = $"{_config.GetSection("CSVFilesLocation").Value}\\dungeonsPL.csv";
            return Helpers.CsvHelper.ReadRecordsFromCSVFile<Dungeon, DungeonMap>(location);
        }

    }
}
