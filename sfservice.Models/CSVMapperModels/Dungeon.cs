using System;
using System.Collections.Generic;
using System.Text;

namespace sfservice.Domain.CSVMapperModels
{
    public class Dungeon
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var otherDungeon = (Dungeon)obj;

            if (otherDungeon == null) return false;

            if (this.Number == otherDungeon.Number
                && this.Name == otherDungeon.Name) return true;

            return false;
        }
    }
}
