using CsvHelper.Configuration;
using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.API.Mappers
{
    public sealed class DungeonMonsterMap : ClassMap<DungeonMonster>
    {
        public DungeonMonsterMap()
        {
            Map(n => n.DungeonNumber).Name("D");
            Map(n => n.Level).Name("DLevel");
            Map(n => n.MonsterName).Name("Opponent");
            Map(n => n.MonsterLevel).Name("Level");
            Map(n => n.Class).Name("Class");
            Map(n => n.Strength).Name("Strength");
            Map(n => n.Dexterity).Name("Dexterity");
            Map(n => n.Intelligence).Name("Intelligence");
            Map(n => n.Constitution).Name("Constitution");
            Map(n => n.Luck).Name("Luck");
            Map(n => n.HitPoints).Name("Hit Points");
            Map(n => n.Experience).Name("Experience");
        }
    }
}
