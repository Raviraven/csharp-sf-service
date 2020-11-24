using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.API.Services
{
    public interface IDungeonService
    {
        List<Dungeon> GetDungeons();
        List<Dungeon> GetDungeon(int dungeonNumber);
        Dungeon GetDungeonMonsterById(int dungeonNumber, int monsterNumber);
    }
}
