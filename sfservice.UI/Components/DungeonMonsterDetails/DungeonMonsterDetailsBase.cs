using Microsoft.AspNetCore.Components;
using sfservice.Models.CSVMapperModels;
using sfservice.UI.API_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.UI.Components.DungeonMonsterDetails
{
    public class DungeonMonsterDetailsBase : ComponentBase
    {
        [Parameter]
        public string DungeonMonsterNumber { get; set; }
        [Parameter]
        public string DungeonNumber { get; set; }


        [Inject]
        public IDungeonsApiService DungeonsApiService { get; set; }

        public DungeonMonster DungeonMonster { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            DungeonMonster = await DungeonsApiService.GetMonsterFromDungeonById(DungeonNumber, DungeonMonsterNumber);
        }
    }
}
