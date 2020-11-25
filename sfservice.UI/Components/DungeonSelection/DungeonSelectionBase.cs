using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using sfservice.Models.CSVMapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.UI.Components
{
    public class DungeonSelectionBase : ComponentBase
    {
        [Parameter]
        public List<Dungeon> Dungeons { get; set; }

        public string ChosenDungeonId { get; set; } = "";
        public EditContext EditContext { get; set; }

        public List<DungeonMonster> DungeonMonsters { get; set; } = new List<DungeonMonster>();
        
        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(ChosenDungeonId);
        }
    }
}
