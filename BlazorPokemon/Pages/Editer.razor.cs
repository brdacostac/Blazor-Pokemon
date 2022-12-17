using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using BlazorPokemon.Models;
using System.Numerics;
using BlazorPokemon.Services;


namespace BlazorPokemon.Pages
{
    public partial class Editer
    {
        [Parameter]
        public int Id { get; set; }

        /// <summary>
        /// The default enchant categories.
        /// </summary>
        private List<string> ElementType = new List<string>() { "Dragon ", "Eau ", "Electrik ", "Feu ", "Glace ", "Plante ", "Psy ", "Ténèbres " };

        /// <summary>
        /// The current Pokemon model
        /// </summary>
        private PokemonModel pokemonModel = new()
        {
            ElementType = new List<string>(),
        };

       

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var pokemon = await DataService.GetById(Id);

            var fileContent = await File.ReadAllBytesAsync($"{WebHostEnvironment.WebRootPath}/images/default.png");

            if (File.Exists($"{WebHostEnvironment.WebRootPath}/images/{pokemonModel.Name}.png"))
            {
                fileContent = await File.ReadAllBytesAsync($"{WebHostEnvironment.WebRootPath}/images/{pokemon.Name}.png");
            }

            // Set the model with the pokemon
            pokemonModel = new PokemonModel
            {
                Id = pokemon.Id,
                DisplayName = pokemon.DisplayName,
                Name = pokemon.Name,
                HealthPoints = pokemon.HealthPoints,
                ElementType = pokemon.ElementType,
                ImageContent = fileContent
            };
        }

        private async void HandleValidSubmit()
        {
            await DataService.Update(Id, pokemonModel);

            NavigationManager.NavigateTo("ListPokemon");
        }

        private async Task LoadImage(InputFileChangeEventArgs e)
        {
            // Set the content of the image to the model
            using (var memoryStream = new MemoryStream())
            {
                await e.File.OpenReadStream().CopyToAsync(memoryStream);
                pokemonModel.ImageContent = memoryStream.ToArray();
            }
        }

        private void OnElementTypeChange(string type, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!pokemonModel.ElementType.Contains(type))
                {
                    pokemonModel.ElementType.Add(type);
                }

                return;
            }

            if (pokemonModel.ElementType.Contains(type))
            {
                pokemonModel.ElementType.Remove(type);
            }
        }

        
    }
}
