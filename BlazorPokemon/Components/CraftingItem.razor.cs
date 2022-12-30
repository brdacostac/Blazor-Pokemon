using Blazorise;
using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace BlazorPokemon.Components
{
    public partial class CraftingItem
    {
        [Parameter]
        public int Index { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Parameter]
        public Pokemon Pokemon { get; set; }

        [Parameter]
        public bool NoDrop { get; set; }

        [CascadingParameter]
        public Crafting Parent { get; set; }


        internal void OnDragEnter()
        {
            if (NoDrop)
            {
                return;
            }

            Parent.Actions.Add(new CraftingAction { Action = "Drag Enter", Pokemon = this.Pokemon, Index = this.Index });
        }

        internal void OnDragLeave()
        {
            if (NoDrop)
            {
                return;
            }

            Parent.Actions.Add(new CraftingAction { Action = "Drag Leave", Pokemon = this.Pokemon, Index = this.Index });
        }

        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }
            if (Parent.CurrentDragItem.HealthPoints > 0 )
            {
                if (Parent.CurrentDragItem == Parent.RecipeItems[(1-this.Index)])
                    return;

                this.Pokemon = Parent.CurrentDragItem;
               
                Parent.RecipeItems[this.Index] = this.Pokemon;

                Parent.Actions.Add(new CraftingAction { Action = "Drop", Pokemon = this.Pokemon, Index = this.Index });
                
                Parent.CheckRecipe();
            }

        }

        private async void HealPokemon()
        {
            this.Pokemon.HealthPoints = 100;
            await DataService.UpdateLoser(this.Pokemon.Id, this.Pokemon);
        }
        private void OnDragStart()
        {
            Parent.CurrentDragItem = this.Pokemon;

            Parent.Actions.Add(new CraftingAction { Action = "Drag Start", Pokemon = this.Pokemon, Index = this.Index });
        }
    }
}
