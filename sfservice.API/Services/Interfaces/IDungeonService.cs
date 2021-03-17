using sfservice.Domain.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.API.Services
{
    public interface IDungeonService
    {
        List<Dungeon> Get();

        List<DungeonMonster> GetAllDungeonMonsters();
        List<DungeonMonster> GetDungeonMonstersById(int dungeonNumber);
        DungeonMonster GetDungeonMonsterById(int dungeonNumber, int monsterNumber);
    }
}
