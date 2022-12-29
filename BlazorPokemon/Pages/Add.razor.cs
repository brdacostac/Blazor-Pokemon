using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using BlazorPokemon.Models;
using System.Numerics;
using BlazorPokemon.Services;

namespace BlazorPokemon.Pages
{
    public partial class Add
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        /// <summary>
        /// The default type categories.
        /// </summary>
        private List<string> elementType = new List<string>() { "Dragon", "Eau", "Electrik", "Feu", "Glace", "Plante", "Psy", "Ténèbres" };


        /// <summary>
        /// The current pokemon model
        /// </summary>
        private PokemonModel pokemonModel = new()
        {
            ElementType = new List<string>()
        };

        [Inject]
        public IDataService DataService { get; set; }


        private async void HandleValidSubmit()
        {
            await DataService.Add(pokemonModel);

            NavigationManager.NavigateTo("list");
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
