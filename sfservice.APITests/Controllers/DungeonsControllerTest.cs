using System;
using System.Collections.Generic;
using System.Text;
using Flurl.Http;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using sfservice.API;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using sfservice.API.Services;
using sfservice.Models.CSVMapperModels;
using Xunit;
using System.Net;

namespace sfservice.APITests.Controllers
{
    internal class FakeDungeonsService : IDungeonService
    {
        public List<Dungeon> Get()
        {
            return new List<Dungeon> {
                    new Dungeon () { Name = "dung 1", Number = 0}
                };
        }

        public List<DungeonMonster> GetAllDungeonMonsters()
        {
            throw new NotImplementedException();
        }

        public DungeonMonster GetDungeonMonsterById(int dungeonNumber, int monsterNumber)
        {
            throw new NotImplementedException();
        }

        public List<DungeonMonster> GetDungeonMonstersById(int dungeonNumber)
        {
            throw new NotImplementedException();
        }
    }

    //integration tests
    public class DungeonsControllerTest
    {
        private IHost createFakeDungeonServiceHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                builder
                    .UseStartup<Startup>()
                    .UseTestServer()
                    .ConfigureTestServices(collection =>
                    {
                        collection.Replace(new ServiceDescriptor(
                            typeof(IDungeonService),
                            typeof(FakeDungeonsService),
                            ServiceLifetime.Singleton
                        ));
                    }
                    )
                ).Build();
        }

        [Fact]
        public async void Get_ShouldReturnDeungeonsList()
        {
            using var host = createFakeDungeonServiceHost();
            await host.StartAsync();

            using var client =
                new FlurlClient(host.GetTestServer().CreateClient());

            var httpResponse = await client.Request("dungeons")
                .AllowAnyHttpStatus()
                .GetAsync();


            var result = new List<Dungeon>();
            using (var stream = await httpResponse.ResponseMessage.Content.ReadAsStreamAsync())
            {
                result = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Dungeon>>
                    (stream, new System.Text.Json.JsonSerializerOptions 
                    { IgnoreNullValues = true, PropertyNameCaseInsensitive = true} );
            }

            var expectedList = new List<Dungeon> {
                    new Dungeon () { Name = "dung 1", Number = 0}
                };

            //httpResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().BeEquivalentTo(expectedList);
        }

        
    }
}
