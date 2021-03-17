using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using sfservice.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Moq;
using sfservice.Domain.CSVMapperModels;
using sfservice.API.Mappers;

namespace sfservice.APITests.Services
{
   public class DungeonServiceTest
    {
        [Fact]
        public void GetDungeons_ReturnsListOfDungeons()
        {
            var configurationMock = new Mock<IConfiguration>();
            var s = Directory.GetCurrentDirectory();
            var sArray = s.Split('\\');
            string dir = "";

            foreach (var n in sArray)
            {
                dir += $"{n}\\";
                if (n == "sf-service") break;
            }


            configurationMock.Setup(n => n["CSVFilesLocation"]).Returns(dir);
            var service = new DungeonService(configurationMock.Object, new CsvService());
            var results = service.GetAllDungeonMonsters();

            Assert.NotNull(results);
            Assert.True(results.Count > 0);
        }

        [Fact]
        public void GetDungeons_ReturnsDungeonsListFromCsvService()
        {
            var csvServiceMoq = new Mock<ICsvService>();
            var configurationMock = new Mock<IConfiguration>();
            var dungeonList = new List<DungeonMonster>();
            dungeonList.Add(new DungeonMonster
            {
                Class = "sorc",
                Constitution = "123",
                Dexterity = "12",
                DungeonNumber = -1,
                Experience = "9020",
                HitPoints = "8723",
                Intelligence = "90",
                Level = 12,
                Luck = "0",
                MonsterLevel = "-3",
                MonsterName = "Some test name",
                Strength = "1"
            });

            configurationMock.Setup(n => n["CSVFilesLocation"]);
            csvServiceMoq.Setup(n=>n.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(It.IsAny<string>()))
                .Returns(dungeonList);

            var service = new DungeonService(configurationMock.Object, csvServiceMoq.Object);
            var results = service.GetAllDungeonMonsters();

            Assert.True(results.Equals(dungeonList));
        }

        [Fact]
        public void GetDungeon_WrongDungeonNumber_ReturnsNull()
        {
            var csvServiceMoq = new Mock<ICsvService>();
            var configurationMock = new Mock<IConfiguration>();
            var dungeonList = new List<DungeonMonster>();
            dungeonList.Add(new DungeonMonster
            {
                Class = "sorc",
                Constitution = "123",
                Dexterity = "12",
                DungeonNumber = -1,
                Experience = "9020",
                HitPoints = "8723",
                Intelligence = "90",
                Level = 12,
                Luck = "0",
                MonsterLevel = "-3",
                MonsterName = "Some test name",
                Strength = "1"
            });

            configurationMock.Setup(n => n["CSVFilesLocation"]);
            csvServiceMoq.Setup(n => n.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(It.IsAny<string>()))
                .Returns(dungeonList);

            var service = new DungeonService(configurationMock.Object, csvServiceMoq.Object);
            var result = service.GetDungeonMonstersById(1);

            Assert.True(result.Count == 0);
        }


        [Fact]
        public void GetDungeon_ExistingDungeonNumber_ReturnsDungeon()
        {
            var csvServiceMoq = new Mock<ICsvService>();
            var configurationMock = new Mock<IConfiguration>();
            var dungeonList = new List<DungeonMonster>();
            var singleDungeonMonster = new DungeonMonster
            {
                Class = "sorc",
                Constitution = "123",
                Dexterity = "12",
                DungeonNumber = -1,
                Experience = "9020",
                HitPoints = "8723",
                Intelligence = "90",
                Level = 12,
                Luck = "0",
                MonsterLevel = "-3",
                MonsterName = "Some test name",
                Strength = "1"
            };

            var singleDungeonMonster2 = new DungeonMonster
            {
                Class = "sorc",
                Constitution = "123",
                Dexterity = "12",
                DungeonNumber = 3,
                Experience = "9020",
                HitPoints = "8723",
                Intelligence = "90",
                Level = 12,
                Luck = "0",
                MonsterLevel = "-3",
                MonsterName = "Some test name",
                Strength = "1"
            };

            var resultList = new List<DungeonMonster>{ singleDungeonMonster };

            dungeonList.Add(singleDungeonMonster);
            dungeonList.Add(singleDungeonMonster2);

            configurationMock.Setup(n => n["CSVFilesLocation"]);
            csvServiceMoq.Setup(n => n.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(It.IsAny<string>()))
                .Returns(dungeonList);

            var service = new DungeonService(configurationMock.Object, csvServiceMoq.Object);
            var result = service.GetDungeonMonstersById(-1);

            var dungeonEquals = result[0].Equals(resultList[0]);

            Assert.True(dungeonEquals);
        }
    }
}
