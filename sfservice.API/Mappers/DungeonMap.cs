using CsvHelper.Configuration;
using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.API.Mappers
{
    public class DungeonMap : ClassMap<Dungeon>
    {
        public DungeonMap()
        {
            Map(n => n.Number).Name("Id");
            Map(n => n.Name).Name("Name");
        }
    }
}
