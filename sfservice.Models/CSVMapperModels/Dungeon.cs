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

        public override bool Equals(object obj)
        {
            var objDungeon = (Dungeon)obj;

            if (objDungeon == null) return false;

            if (objDungeon.DungeonNumber == this.DungeonNumber
                && objDungeon.Level == this.Level
                && objDungeon.MonsterName.Equals(this.MonsterName)
                && objDungeon.MonsterLevel == this.MonsterLevel
                && objDungeon.Class.Equals(this.Class)
                && objDungeon.Strength.Equals(this.Strength)
                && objDungeon.Dexterity.Equals(this.Dexterity)
                && objDungeon.Intelligence.Equals(this.Intelligence)
                && objDungeon.Constitution.Equals(this.Constitution)
                && objDungeon.Luck.Equals(this.Luck)
                && objDungeon.HitPoints.Equals(this.HitPoints)
                && objDungeon.Experience.Equals(this.Experience)) return true;

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
