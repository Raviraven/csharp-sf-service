using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.UI.API_Services
{
    public interface IDungeonsApiService
    {
        Task<List<Dungeon>> Get();
        Task<List<DungeonMonster>> GetDungeonWithMonstersById(string dungeonId);
        Task<DungeonMonster> GetMonsterFromDungeonById(string dungeonId, string monsterId);
    }
}
