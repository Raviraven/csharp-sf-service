using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.UI.API_Services
{
    public interface IDungeonsApiService
    {
        Task<List<Dungeon>> Get();
    }
}
