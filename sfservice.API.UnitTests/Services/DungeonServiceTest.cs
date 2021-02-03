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
using FluentAssertions;
using Bogus;

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

            var dungeonList = new Faker<DungeonMonster>()
                .RuleFor(n=>n.Class, b=>b.Random.String(10))
                .RuleFor(n=>n.Constitution, b=>b.Random.String())
                .RuleFor(n=>n.Dexterity, b=>b.Random.String())
                .RuleFor(n=>n.DungeonNumber, b=>b.Random.Int())
                .RuleFor(n=>n.Experience, b=>b.Random.String())
                .RuleFor(n=>n.HitPoints, b=>b.Random.String())
                .RuleFor(n=>n.Intelligence, b=>b.Random.String())
                .RuleFor(n=>n.Level, b=>b.Random.Int())
                .RuleFor(n=>n.Luck, b=>b.Random.String())
                .RuleFor(n=>n.MonsterLevel, b=>b.Random.String())
                .RuleFor(n=>n.MonsterName, b=>b.Random.String())
                .RuleFor(n=>n.Strength, b=>b.Random.String())
                .Generate(10);

            configurationMock.Setup(n => n["CSVFilesLocation"]);
            csvServiceMoq.Setup(n=>n.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(It.IsAny<string>()))
                .Returns(dungeonList);

            var service = new DungeonService(configurationMock.Object, csvServiceMoq.Object);
            var results = service.GetAllDungeonMonsters();

            results.Should()
                .BeEquivalentTo(dungeonList);
        }

        [Fact]
        public void GetDungeon_WrongDungeonNumber_ReturnsNull()
        {
            var csvServiceMoq = new Mock<ICsvService>();
            var configurationMock = new Mock<IConfiguration>();

            var dungeonList = new Faker<DungeonMonster>()
                .RuleFor(n => n.Class, b => b.Random.String(10))
                .RuleFor(n => n.Constitution, b => b.Random.String())
                .RuleFor(n => n.Dexterity, b => b.Random.String())
                .RuleFor(n => n.DungeonNumber, () => -1)
                .RuleFor(n => n.Experience, b => b.Random.String())
                .RuleFor(n => n.HitPoints, b => b.Random.String())
                .RuleFor(n => n.Intelligence, b => b.Random.String())
                .RuleFor(n => n.Level, b => b.Random.Int())
                .RuleFor(n => n.Luck, b => b.Random.String())
                .RuleFor(n => n.MonsterLevel, b => b.Random.String())
                .RuleFor(n => n.MonsterName, b => b.Random.String())
                .RuleFor(n => n.Strength, b => b.Random.String())
                .Generate(10);

            configurationMock.Setup(n => n["CSVFilesLocation"]);
            csvServiceMoq.Setup(n => n.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(It.IsAny<string>()))
                .Returns(dungeonList);

            var service = new DungeonService(configurationMock.Object, csvServiceMoq.Object);
            var result = service.GetDungeonMonstersById(1);

            result.Should().BeEmpty();

        }


        [Fact]
        public void GetDungeon_ExistingDungeonNumber_ReturnsDungeon()
        {
            var csvServiceMoq = new Mock<ICsvService>();
            var configurationMock = new Mock<IConfiguration>();

            var invalidDungeonsList = new Faker<DungeonMonster>()
                .RuleFor(n => n.Class, b => b.Random.String(10))
                .RuleFor(n => n.Constitution, b => b.Random.String())
                .RuleFor(n => n.Dexterity, b => b.Random.String())
                .RuleFor(n => n.DungeonNumber, b=>b.Random.Int(min: 1))
                .RuleFor(n => n.Experience, b => b.Random.String())
                .RuleFor(n => n.HitPoints, b => b.Random.String())
                .RuleFor(n => n.Intelligence, b => b.Random.String())
                .RuleFor(n => n.Level, b => b.Random.Int())
                .RuleFor(n => n.Luck, b => b.Random.String())
                .RuleFor(n => n.MonsterLevel, b => b.Random.String())
                .RuleFor(n => n.MonsterName, b => b.Random.String())
                .RuleFor(n => n.Strength, b => b.Random.String())
                .Generate(10);

            var validDungeonsList = new Faker<DungeonMonster>()
                .RuleFor(n => n.Class, b => b.Random.String(10))
                .RuleFor(n => n.Constitution, b => b.Random.String())
                .RuleFor(n => n.Dexterity, b => b.Random.String())
                .RuleFor(n => n.DungeonNumber, () => -1)
                .RuleFor(n => n.Experience, b => b.Random.String())
                .RuleFor(n => n.HitPoints, b => b.Random.String())
                .RuleFor(n => n.Intelligence, b => b.Random.String())
                .RuleFor(n => n.Level, b => b.Random.Int())
                .RuleFor(n => n.Luck, b => b.Random.String())
                .RuleFor(n => n.MonsterLevel, b => b.Random.String())
                .RuleFor(n => n.MonsterName, b => b.Random.String())
                .RuleFor(n => n.Strength, b => b.Random.String())
                .Generate(10);

            var dungeonList = new List<DungeonMonster>();
            dungeonList.AddRange(invalidDungeonsList);
            dungeonList.AddRange(validDungeonsList);


            configurationMock.Setup(n => n["CSVFilesLocation"]);
            csvServiceMoq.Setup(n => n.ReadRecordsFromCSVFile<DungeonMonster, DungeonMonsterMap>(It.IsAny<string>()))
                .Returns(dungeonList);

            var service = new DungeonService(configurationMock.Object, csvServiceMoq.Object);
            var result = service.GetDungeonMonstersById(-1);

            result.Should().BeEquivalentTo(validDungeonsList);
        }
    }
}
