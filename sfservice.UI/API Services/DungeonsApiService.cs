using Microsoft.AspNetCore.Components;
using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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
            baseUrl = _httpClient.BaseAddress.AbsoluteUri;
        }

        public async Task<List<Dungeon>> Get()
        {
            try
            {
                using (var response = await _httpClient.GetAsync($"{baseUrl}/dungeons"))
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
                        return null;
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
