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
using Microsoft.AspNetCore.Components;

namespace sfservice.API.Services
{
    public class DungeonService : IDungeonService
    {
        private readonly IConfiguration _config;

        [Inject]
        private ICsvService csvService { get; set; }

        public DungeonService(IConfiguration config)
        {
            _config = config;
        }

        public DungeonService(IConfiguration config, ICsvService csvService)
        {
            _config = config;
            this.csvService = csvService;
        }

        public List<Dungeon> GetDungeons()
        {
            var location = $"{_config["CSVFilesLocation"]}\\dungeonsPL.csv";
            return csvService.ReadRecordsFromCSVFile<Dungeon, DungeonMap>(location);
        }

        public List<Dungeon> GetDungeon(int dungeonNumber)
        {
            var location = $"{_config["CSVFilesLocation"]}\\dungeonsPL.csv";
            var allDungeons = csvService.ReadRecordsFromCSVFile<Dungeon, DungeonMap>(location);

            return allDungeons.Where(d => d.DungeonNumber == dungeonNumber).ToList();
        }

        public Dungeon GetDungeonMonsterById(int dungeonNumber, int monsterNumber)
        {
            var location = $"{_config["CSVFilesLocation"]}\\dungeonsPL.csv";
            var allDungeons = csvService.ReadRecordsFromCSVFile<Dungeon, DungeonMap>(location);

            return allDungeons.FirstOrDefault(d => d.DungeonNumber == dungeonNumber && d.Level == monsterNumber);
        }

    }
}
