using System;
using System.Collections.Generic;
using System.Text;

namespace sfservice.Models.CSVMapperModels
{
    public class DungeonMonster
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

        public override bool Equals(object obj)
        {
            var objDungeon = (DungeonMonster)obj;

            if (objDungeon == null) return false;

            if (objDungeon.DungeonNumber == this.DungeonNumber
                && objDungeon.Level == this.Level
                && objDungeon.MonsterName == this.MonsterName
                && objDungeon.MonsterLevel == this.MonsterLevel
                && objDungeon.Class == this.Class
                && objDungeon.Strength == this.Strength
                && objDungeon.Dexterity == this.Dexterity
                && objDungeon.Intelligence == this.Intelligence
                && objDungeon.Constitution == this.Constitution
                && objDungeon.Luck == this.Luck
                && objDungeon.HitPoints == this.HitPoints
                && objDungeon.Experience == this.Experience) return true;

            return false;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(DungeonNumber);
            hash.Add(Level);
            hash.Add(MonsterName);
            hash.Add(MonsterLevel);
            hash.Add(Class);
            hash.Add(Strength);
            hash.Add(Dexterity);
            hash.Add(Intelligence);
            hash.Add(Constitution);
            hash.Add(Luck);
            hash.Add(HitPoints);
            hash.Add(Experience);
            return hash.ToHashCode();
        }
    }
}
