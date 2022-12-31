using Blazorise;
using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json;


namespace BlazorPokemon.Components
{
    public partial class Inventory
    {
        [Inject]
        public IStringLocalizer<Inventory> Localizer { get; set; }

        [Inject]
        public IDataService DataService { get; set; }
        public ObservableCollection<InventoryAction> Actions { get; }

        private bool choiceSort = false;
        public List<Pokemon> RecipeItems { get; set; }

        public Pokemon CurrentDragItem { get; set; }

        [Parameter]
        public List<Pokemon> Pokemons { get; set; }

        /// <summary>
        /// Gets or sets the java script runtime.
        /// </summary>
        [Inject]
        internal IJSRuntime JavaScriptRuntime { get; set; }

        public Inventory()
        {
            Actions = new ObservableCollection<InventoryAction>();
            Actions.CollectionChanged += OnActionsCollectionChanged;

            string fileName = "wwwroot/Inventory.json";
            string jsonString = File.ReadAllText(fileName);
            this.RecipeItems = JsonSerializer.Deserialize<List<Pokemon>>(jsonString)!;
        }

        private void SortByame()
        {
            if (choiceSort)
            {
                Pokemons.Sort((x, y) => x.Id.CompareTo(y.Id));
                choiceSort = !choiceSort;
                Actions.Add(new InventoryAction { Action = "Sort by Id" });
            }
            else
            {
                Pokemons.Sort((x, y) => x.Name.CompareTo(y.Name));
                choiceSort = !choiceSort;
                Actions.Add(new InventoryAction { Action = "Sort by Name" });
            }
        }

        public void Save()
        {
            string fileName = "wwwroot/Inventory.json";
            string jsonString = JsonSerializer.Serialize(RecipeItems);
            File.WriteAllText(fileName, jsonString);
        }

        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("Inventory.AddActions", e.NewItems);
        }

        protected override async Task OnInitializedAsync()
        {
            int size = await DataService.Count();
            Pokemons = await DataService.List(0, size);

        }
    }
}
