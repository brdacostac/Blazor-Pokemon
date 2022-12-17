using BlazorPokemon.Modals;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Blazored.LocalStorage;

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
        public IModalService Modal { get; set; }

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

            // When you use a real API, we use this follow code
            //var response = await Http.GetJsonAsync<Data[]>( $"http://my-api/api/data?page={e.Page}&pageSize={e.PageSize}" );
            var response = (await LocalStorage.GetItemAsync<Pokemon[]>("data")).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();

            if (!e.CancellationToken.IsCancellationRequested)
            {
                totalPokemon = (await LocalStorage.GetItemAsync<List<Pokemon>>("data")).Count;
                pokemons = new List<Pokemon>(response); // an actual data for the current page
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

