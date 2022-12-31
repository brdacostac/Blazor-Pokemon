using BlazorPokemon.Modals;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Blazored.LocalStorage;
using Blazorise;
using System.Runtime.Serialization;

namespace BlazorPokemon.Pages
{
    
    public partial class ListPokemon
    {
        [Inject]
        public IStringLocalizer<ListPokemon> Localizer { get; set; }

        private List<Pokemon> pokemons;


        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public Blazored.Modal.Services.IModalService Modal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int size = await DataService.Count();
            pokemons = await DataService.List(0, size);

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (!firstRender)
            {
                return;
            }

            var currentData = await LocalStorage.GetItemAsync<Pokemon[]>("data");


            if (currentData == null)
            {
                var originalData = Http.GetFromJsonAsync<Pokemon[]>($"{NavigationManager.BaseUri}pokemon-data.json").Result;
                await LocalStorage.SetItemAsync("data", originalData);
            }
        }

        private async void OnDelete(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(Pokemon.Id), id);

            var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
            var result = await modal.Result;

            if (result.Cancelled)
            {
                return;
            }

            await DataService.Delete(id);

            // Reload the page
            NavigationManager.NavigateTo("list", true);
        }

    }

}

