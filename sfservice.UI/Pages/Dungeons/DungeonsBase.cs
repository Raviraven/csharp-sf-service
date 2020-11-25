using Microsoft.AspNetCore.Components;
using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sfservice.UI.API_Services;

namespace sfservice.UI.Pages.Dungeons
{
    public class DungeonsBase : ComponentBase
    {
        [Inject]
        public IDungeonsApiService DungeonsApiService { get; set; }

        protected List<Dungeon> DungeonsList;


        protected override async Task OnInitializedAsync()
        {
            var dungeons = await DungeonsApiService.Get();
            DungeonsList = dungeons;
        }
    }
}
