using Blazorise;
using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Reflection;

namespace BlazorPokemon.Components
{
    public partial class InventoryItem
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
        public Inventory Parent { get; set; }


        internal void OnDragEnter()
        {
            if (NoDrop)
            {
                return;
            }

            Parent.Actions.Add(new InventoryAction { Action = "Drag Enter", Pokemon = this.Pokemon, Index = this.Index });
        }

        internal void OnDragLeave()
        {
            if (NoDrop)
            {
                return;
            }

            Parent.Actions.Add(new InventoryAction { Action = "Drag Leave", Pokemon = this.Pokemon, Index = this.Index });
        }

        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }
            
            if (this.Parent.CurrentDragItem!= null) { 
                if(!Parent.RecipeItems.Any(i => i != null && i.Id == Parent.CurrentDragItem.Id)) {
                    this.Pokemon = Parent.CurrentDragItem;
                    Parent.Actions.Add(new InventoryAction { Action = "Drop", Pokemon = this.Pokemon, Index = this.Index });

                    Parent.RecipeItems[this.Index] = this.Pokemon;
                    Parent.Save();
                }
            }
        }

        private void OnDragStart()
        {
            Parent.CurrentDragItem = this.Pokemon;

            Parent.Actions.Add(new InventoryAction { Action = "Drag Start", Pokemon = this.Pokemon, Index = this.Index });
        }

        private void OnDragEnd()
        {
            if (Parent.Actions.Last().Action == "Drag Leave")
            {
                this.Pokemon = null;
                Parent.Actions.Add(new InventoryAction { Action = "Delete", Pokemon = this.Pokemon, Index = this.Index });

                Parent.RecipeItems[this.Index] = null;
                Parent.Save();
            }
            
        }
    }
}
