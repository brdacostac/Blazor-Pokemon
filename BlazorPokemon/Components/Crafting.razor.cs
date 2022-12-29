using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BlazorPokemon.Components
{
    public partial class Crafting
    {
        private Pokemon _recipeResult;

        private List<CraftingItem> refPokemon = new List<CraftingItem> { new (), new() };



        [Inject]
        public IDataService DataService { get; set; }

        public Crafting()
        {
            Actions = new ObservableCollection<CraftingAction>();
            Actions.CollectionChanged += OnActionsCollectionChanged;
            this.RecipeItems = new List<Pokemon> { null, null };
        }

        public ObservableCollection<CraftingAction> Actions { get; set; }
        public Pokemon CurrentDragItem { get; set; }

        [Parameter]
        public List<Pokemon> Pokemons { get; set; }

        public List<Pokemon> RecipeItems { get; set; }

        public Pokemon RecipeResult
        {
            get => this._recipeResult;
            set
            {
                if (this._recipeResult == value)
                {
                    return;
                }

                this._recipeResult = value;
                this.StateHasChanged();
            }
        }

        /// <summary>
        /// Gets or sets the java script runtime.
        /// </summary>
        [Inject]
        internal IJSRuntime JavaScriptRuntime { get; set; }


        public void CheckRecipe()
        {
            RecipeResult = null;

            if (RecipeItems[0]!=null && RecipeItems[1] != null)
            {
                int winner = Pokemon.compareType(RecipeItems[0], RecipeItems[1]);
                RecipeResult = RecipeItems[winner];


                if (RecipeItems[1 - winner].HealthPoints <= 0)
                {
                    RecipeItems[1 - winner] = null;
                    refPokemon[1 - winner].Pokemon = null;
                    RecipeResult = null;
                }
                
            }
            else
            {
                return;
            }

        }

        protected override async Task OnInitializedAsync()
        {
            Pokemons = await DataService.All();

        }
        
        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
        }
    }
}
