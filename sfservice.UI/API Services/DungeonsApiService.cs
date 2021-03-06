﻿using Microsoft.AspNetCore.Components;
using sfservice.Domain.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using sfservice.Domain.Exceptions;

namespace sfservice.UI.API_Services
{
    public class DungeonsApiService : IDungeonsApiService
    {
        //[Inject]
        private HttpClient _httpClient { get; set; }
        private string baseUrl = "";

        public DungeonsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            baseUrl = $"{_httpClient.BaseAddress.AbsoluteUri}/dungeons";
        }

        public async Task<List<Dungeon>> Get()
        {
            try
            {
                using (var response = await _httpClient.GetAsync($"{baseUrl}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            return await JsonSerializer.DeserializeAsync<List<Dungeon>>
                                (stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, IgnoreNullValues = true });
                        }
                    }
                    else
                    {
                        throw new UnsuccessfullApiStatusCodeException($"{(int) response.StatusCode} {response.ReasonPhrase}");
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<DungeonMonster>> GetDungeonWithMonstersById(string dungeonId)
        {
            try
            {
                using (var response = await _httpClient.GetAsync($"{baseUrl}/{dungeonId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            return await JsonSerializer.DeserializeAsync<List<DungeonMonster>>
                                (stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, IgnoreNullValues = true });
                        }
                    }
                    else
                    {
                        throw new UnsuccessfullApiStatusCodeException($"{(int)response.StatusCode} {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DungeonMonster> GetMonsterFromDungeonById(string dungeonId, string monsterId)
        {
            try
            {
                using (var response = await _httpClient.GetAsync($"{baseUrl}/{dungeonId}/{monsterId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            return await JsonSerializer.DeserializeAsync<DungeonMonster>
                                (stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, IgnoreNullValues = true });
                        }
                    }
                    else
                    {
                        throw new UnsuccessfullApiStatusCodeException($"{(int)response.StatusCode} {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
