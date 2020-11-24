using System;
using System.Collections.Generic;
using System.Text;

namespace sfservice.Models.CSVMapperModels
{
    public class Dungeon
    {
        public int DungeonNumber { get; set; }
        public int Level { get; set; }
        public string MonsterName { get; set; }
        public int MonsterLevel { get; set; }
        public string Class { get; set; }
        public string Strength { get; set; }
        public string Dexterity { get; set; }
        public string Intelligence { get; set; }
        //shouldn't it be durability? 
        public string Constitution { get; set; }
        public string Luck { get; set; }
        public string HitPoints { get; set; }
        //double?
        public string Experience { get; set; }
    }
}
