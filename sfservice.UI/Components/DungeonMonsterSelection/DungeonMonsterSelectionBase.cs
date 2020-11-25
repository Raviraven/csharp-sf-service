using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using sfservice.Models.CSVMapperModels;
using sfservice.UI.API_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.UI.Components
{
    public class DungeonMonsterSelectionBase : ComponentBase
    {
        [Parameter]
        public string DungeonNumber { get; set; }

        [Inject]
        public IDungeonsApiService DungeonsApiService { get; set; }

        public List<DungeonMonster> DungeonMonsters { get; set; }
        public string MonsterNumber { get; set; } = "";
        public EditContext EditContext { get; set; }

        public DungeonMonster ChosenMonster { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(MonsterNumber);
        }

        protected override async Task OnParametersSetAsync()
        {
            var dungeonMonsters = await DungeonsApiService.GetDungeonWithMonstersById(DungeonNumber);
            DungeonMonsters = dungeonMonsters;
        }

        protected void SetMonsterDetails() 
        {
            int dungeonNumber = 0;
            int.TryParse(DungeonNumber, out dungeonNumber);
            ChosenMonster = DungeonMonsters.First(n => n.DungeonNumber == dungeonNumber);
            StateHasChanged();
        }
    }
}
