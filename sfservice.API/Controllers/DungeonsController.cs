using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sfservice.API.Services;
using sfservice.Domain.CSVMapperModels;

namespace sfservice.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DungeonsController : ControllerBase 
    { 
    
        private IDungeonService _dungeonService { get; set; }


        public DungeonsController(IDungeonService dungeonService)
        {
            _dungeonService = dungeonService;
        }


        [HttpGet]
        public List<Dungeon> Get()
        {
            return _dungeonService.Get();
        }

        [HttpGet("all")]
        public List<DungeonMonster> GetDungeonMonsters()
        {
            return _dungeonService.GetAllDungeonMonsters();
        }

        [HttpGet("{dungeonId}")]
        public List<DungeonMonster> GetDungeonMonsterById(int dungeonId)
        {
            return _dungeonService.GetDungeonMonstersById(dungeonId);
        }

        [HttpGet("{dungeonNumber}/{monsterNumber}")]
        public DungeonMonster GetDungeonMonster(int dungeonNumber, int monsterNumber)
        {
            return _dungeonService.GetDungeonMonsterById(dungeonNumber, monsterNumber);
        }
    }
}
