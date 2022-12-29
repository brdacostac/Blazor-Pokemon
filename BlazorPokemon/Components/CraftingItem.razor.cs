using Blazorise;
using BlazorPokemon.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorPokemon.Components
{
    public partial class CraftingItem
    {
        [Parameter]
        public int Index { get; set; }

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
            if (Parent.CurrentDragItem.HealthPoints > 0)
            {
                this.Pokemon = Parent.CurrentDragItem;

                Parent.RecipeItems[this.Index] = this.Pokemon;

                Parent.Actions.Add(new CraftingAction { Action = "Drop", Pokemon = this.Pokemon, Index = this.Index });
                Parent.CheckRecipe();
            }
                
            

        }

        private void OnDragStart()
        {
            Parent.CurrentDragItem = this.Pokemon;

            Parent.Actions.Add(new CraftingAction { Action = "Drag Start", Pokemon = this.Pokemon, Index = this.Index });
        }
    }
}
