using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using sfservice.API.Mappers;
using sfservice.Domain.CSVMapperModels;
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
        private string baseCsvFilesLocation { get; set; }

        public DungeonService(IConfiguration config)
        {
            _config = config;
        }

        public DungeonService(IConfiguration config, ICsvService csvService)
        {
            _config = config;
            this.csvService = csvService;
            baseCsvFilesLocation = _config["CSVFilesLocation"];
        }

        public List<Dungeon> Get()
        {
            string location = $"{baseCsvFilesLocation}\\dungeonsPL.csv";
            return csvService.ReadRecordsFromCSVFile<Dungeon, DungeonMap>(location);
        }

        public List<DungeonMonster> GetAllDungeonMonsters()
        {
            var location = $"{_config["CSVFilesLocation"]}\\dungeonMonstersPL.csv";
            return csvService.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(location);
        }

        public List<DungeonMonster> GetDungeonMonstersById(int dungeonNumber)
        {
            var location = $"{_config["CSVFilesLocation"]}\\dungeonMonstersPL.csv";
            var allDungeons = csvService.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(location);

            return allDungeons.Where(d => d.DungeonNumber == dungeonNumber).ToList();
        }

        public DungeonMonster GetDungeonMonsterById(int dungeonNumber, int monsterNumber)
        {
            var location = $"{_config["CSVFilesLocation"]}\\dungeonMonstersPL.csv";
            var allDungeons = csvService.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(location);

            return allDungeons.FirstOrDefault(d => d.DungeonNumber == dungeonNumber && d.Level == monsterNumber);
        }

       

        //public 
    }
}
