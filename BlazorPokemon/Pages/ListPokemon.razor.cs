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

namespace BlazorPokemon.Pages
{
    
    public partial class ListPokemon
    {
        [Inject]
        public IStringLocalizer<ListPokemon> Localizer { get; set; }

        private List<Pokemon> pokemons;

        private int totalPokemon;

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

        private async Task OnReadData(DataGridReadDataEventArgs<Pokemon> e)
        {
            if (e.CancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (!e.CancellationToken.IsCancellationRequested)
            {
                pokemons = await DataService.List(e.Page, e.PageSize);
                totalPokemon = await DataService.Count();
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
            NavigationManager.NavigateTo("listpokemon", true);
        }

    }

}

