using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Net.Http;
using sfservice.Domain.CSVMapperModels;
using System.Text.Json;
using System.Threading.Tasks;
using sfservice.UI.API_Services;
using Moq.Protected;
using System.Threading;
using sfservice.Domain.Exceptions;

namespace sfservice.UITests.API_Services
{
    public class DungeonsApiServiceTest
    {
        private Mock<HttpMessageHandler> httpMessageHandlerMocked { get; set; }

        public DungeonsApiServiceTest()
        {
            httpMessageHandlerMocked = new Mock<HttpMessageHandler>();

        }

        private void mockHttpMessageHandler(HttpResponseMessage httpResponseMessage)
        {
            httpMessageHandlerMocked.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);
        }

        [Fact]
        public async void Get_ReturnsListOfDungeons()
        {
            List<Dungeon> dungeons = new List<Dungeon> { 
                new Dungeon{ Number = -2, Name = "First dungeon" },
                new Dungeon{ Number = -1, Name = "Darkest dungeon" },
                new Dungeon{ Number = 0, Name = "THE DUNGEON" },
            };


            var dungeonJson = JsonSerializer.Serialize(dungeons);
            var httpContent = new StringContent(dungeonJson);
            var httpResponse = new HttpResponseMessage { 
                Content = httpContent
            };

            mockHttpMessageHandler(httpResponse);

            var httpClient = new HttpClient(httpMessageHandlerMocked.Object) 
                            { BaseAddress = new Uri("http://localhost/sfservice/") };
            var service = new DungeonsApiService(httpClient);

            var result = await service.Get();

            bool dungeonListsEqual = false;
            if (dungeons.Count == result.Count) 
            {
                dungeonListsEqual = true;
                for (int i=0; i<result.Count; i++)
                {
                    if (!result[i].Equals(dungeons[i])) 
                    {
                        dungeonListsEqual = false;
                        break; 
                    }
                }
            }

            Assert.True(dungeonListsEqual);
        }

        [Fact]
        public async void Get_ReceivesApiError_ThrowsUnsuccessfulApiStatusCodeException()
        {
            var httpResponse = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest };
            mockHttpMessageHandler(httpResponse);

            var httpClient = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(httpClient);

            await Assert.ThrowsAsync<UnsuccessfullApiStatusCodeException>(() => service.Get());
        }

        [Fact]
        public async Task Get_HttpThrowsException_ThrowsException()
        {
            httpMessageHandlerMocked.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ThrowsAsync(new Exception("Mock exception"));

            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(client);

            await Assert.ThrowsAsync<Exception>(() => service.Get());
        }

        [Fact]
        public async Task Get_ReturnsWrongHttpContent_ThrowsJsonException()
        {
            string content = "different content than list<dungeon>";
            
            var httpContent = new StringContent(content);
            var httpResponse = new HttpResponseMessage
            {
                Content = httpContent
            };

            mockHttpMessageHandler(httpResponse);
            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(client);

            await Assert.ThrowsAsync<JsonException>(() => service.Get());
        }


        [Fact]
        public async Task GetDungeonWithMonstersById_GetsDungeonId_ReturnsDungeonWithMonsters()
        {
            var resultList = new List<DungeonMonster> { 
                new DungeonMonster { Class = "some class", Constitution = "123", Dexterity = "444", DungeonNumber = 1,  Experience = "12" },
                new DungeonMonster { Class = "some class", Constitution = "123", Dexterity = "444", DungeonNumber = 1,  Experience = "12", Level = 91, Luck = "1233323" },
            };
            var resultListJson = JsonSerializer.Serialize(resultList);
            var httpContent = new StringContent(resultListJson);
            var httpResponse = new HttpResponseMessage() { Content = httpContent };

            mockHttpMessageHandler(httpResponse);
            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };

            var service = new DungeonsApiService(client);

            var result = await service.GetDungeonWithMonstersById("id");

            bool resultListsEqual = false;

            if (result.Count == resultList.Count) 
            {
                resultListsEqual = true;
                for (int i = 0; i < resultList.Count; i++)
                {
                    if (!result[i].Equals(resultList[i]))
                    {
                        resultListsEqual = false;
                        break;
                    }
                }
            }

            Assert.True(resultListsEqual);
        }

        [Fact]
        public async Task GetDungeonWithMonstersById_ReturnsWrongHttpContent_ThrowsJsonException()
        {
            string content = "different content than list<dungeonmonster>";

            var httpContent = new StringContent(content);
            var httpResponse = new HttpResponseMessage
            {
                Content = httpContent
            };

            mockHttpMessageHandler(httpResponse);
            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(client);

            await Assert.ThrowsAsync<JsonException>(() => service.GetDungeonWithMonstersById(""));
        }

        [Fact]
        public async Task GetDungeonWithMonstersById_ThrowsUnsuccessfulApiStatusCodeException()
        {
            var httpResponse = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest };
            mockHttpMessageHandler(httpResponse);

            var httpClient = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(httpClient);

            await Assert.ThrowsAsync<UnsuccessfullApiStatusCodeException>(() => service.GetDungeonWithMonstersById("id"));
        }

        [Fact]
        public async Task GetDungeonWithMonstersById_HttpThrowsException_ThrowsException()
        {
            httpMessageHandlerMocked.Protected()
                         .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                         .ThrowsAsync(new Exception("Mock exception"));

            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(client);

            await Assert.ThrowsAsync<Exception>(() => service.GetDungeonWithMonstersById("id"));
        }



        [Fact]
        public async Task GetMonsterFromDungeonById_GetIds_ReturnsDungeonMonster()
        {
            var resultDungeonMonster = new DungeonMonster { Class = "some class", Constitution = "123", Dexterity = "444", DungeonNumber = 1, Experience = "12" };
            var resultListJson = JsonSerializer.Serialize(resultDungeonMonster);
            var httpContent = new StringContent(resultListJson);
            var httpResponse = new HttpResponseMessage() { Content = httpContent };

            mockHttpMessageHandler(httpResponse);
            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };

            var service = new DungeonsApiService(client);

            var result = await service.GetMonsterFromDungeonById("dungeon id", "monster id");

            Assert.True(result.Equals(resultDungeonMonster));
        }

        [Fact]
        public async Task GetMonsterFromDungeonById_ReturnsWrongHttpContent_ThrowsJsonException()
        {
            string content = "different content than list<dungeonmonster>";

            var httpContent = new StringContent(content);
            var httpResponse = new HttpResponseMessage
            {
                Content = httpContent
            };

            mockHttpMessageHandler(httpResponse);
            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(client);

            await Assert.ThrowsAsync<JsonException>(() => service.GetMonsterFromDungeonById("dungeon id", "monster id"));
        }

        [Fact]
        public async Task GetMonsterFromDungeonById_ThrowsUnsuccessfulApiStatusCodeException()
        {
            var httpResponse = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest };
            mockHttpMessageHandler(httpResponse);

            var httpClient = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(httpClient);

            await Assert.ThrowsAsync<UnsuccessfullApiStatusCodeException>(() => service.GetMonsterFromDungeonById("dungeon id", "monster id"));
        }

        [Fact]
        public async Task GetMonsterFromDungeonById_HttpThrowsException_ThrowsException()
        {
            httpMessageHandlerMocked.Protected()
                         .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                         .ThrowsAsync(new Exception("Mock exception"));

            var client = new HttpClient(httpMessageHandlerMocked.Object) { BaseAddress = new Uri("http://test") };
            var service = new DungeonsApiService(client);

            await Assert.ThrowsAsync<Exception>(() => service.GetMonsterFromDungeonById("dungeon id", "monster id"));
        }

    }
}
