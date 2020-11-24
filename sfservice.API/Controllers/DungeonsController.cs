using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sfservice.API.Services;
using sfservice.Models.CSVMapperModels;

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
            return _dungeonService.GetDungeons();
        }
    }
}
