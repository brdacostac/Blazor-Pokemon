using Blazored.LocalStorage;
using BlazorPokemon.Factories;
using BlazorPokemon.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorPokemon.Services
{
    public class DataLocalService : IDataService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DataLocalService(
            ILocalStorageService localStorage,
            HttpClient http,
            IWebHostEnvironment webHostEnvironment,
            NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _http = http;
            _webHostEnvironment = webHostEnvironment;
            _navigationManager = navigationManager;
        }

        public async Task Add(PokemonModel model)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Pokemon>>("data");

            // Simulate the Id
            model.Id = currentData.Max(s => s.Id) + 1;

            // Add the pokemon to the current data
            currentData.Add(PokemonFactory.Create(model));

            // Save the image
            var imagePathInfo = new DirectoryInfo($"{_webHostEnvironment.WebRootPath}/images");

            // Check if the folder "images" exist
            if (!imagePathInfo.Exists)
            {
                imagePathInfo.Create();
            }

            // Determine the image name
            var fileName = new FileInfo($"{imagePathInfo}/{model.Name}.png");

            // Write the file content
            await File.WriteAllBytesAsync(fileName.FullName, model.ImageContent);

            // Save the data
            await _localStorage.SetItemAsync("data", currentData);
        }

        public async Task<int> Count()
        {
            // Load data from the local storage
            var currentData = await _localStorage.GetItemAsync<Pokemon[]>("data");

            // Check if data exist in the local storage
            if (currentData == null)
            {
                // this code add in the local storage the pokemon data
                var originalData = await _http.GetFromJsonAsync<Pokemon[]>($"{_navigationManager.BaseUri}pokemon-data.json");
                await _localStorage.SetItemAsync("data", originalData);
            }

            return (await _localStorage.GetItemAsync<Pokemon[]>("data")).Length;
        }

        public async Task<List<Pokemon>> List(int currentPage, int pageSize)
        {
            // Load data from the local storage
            var currentData = await _localStorage.GetItemAsync<Pokemon[]>("data");

            // Check if data exist in the local storage
            if (currentData == null)
            {
                // this code add in the local storage the pokemon data
                var originalData = await _http.GetFromJsonAsync<Pokemon[]>($"{_navigationManager.BaseUri}pokemon-data.json");
                await _localStorage.SetItemAsync("data", originalData);
            }

            return (await _localStorage.GetItemAsync<Pokemon[]>("data")).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }


        public async Task<Pokemon> GetById(int id)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Pokemon>>("data");

            // Get the pokemon int the list
            var pokemon = currentData.FirstOrDefault(w => w.Id == id);

            // Check if pokemon exist
            if (pokemon == null)
            {
                throw new Exception($"Unable to found the pokemon with ID: {id}");
            }

            return pokemon;
        }

        public async Task Update(int id, PokemonModel model)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Pokemon>>("data");

            // Get the pokemon int the list
            var pokemon = currentData.FirstOrDefault(w => w.Id == id);

            // Check if pokemon exist
            if (pokemon == null)
            {
                throw new Exception($"Unable to found the pokemon with ID: {id}");
            }

             // Save the image
            var imagePathInfo = new DirectoryInfo($"{_webHostEnvironment.WebRootPath}/images");

            // Check if the folder "images" exist
            if (!imagePathInfo.Exists)
            {
                imagePathInfo.Create();
            }

            // Delete the previous image
            if (pokemon.Name != model.Name)
            {
                var oldFileName = new FileInfo($"{imagePathInfo}/{pokemon.Name}.png");

                if (oldFileName.Exists)
                {
                    File.Delete(oldFileName.FullName);
                }
            }

            // Determine the image name
            var fileName = new FileInfo($"{imagePathInfo}/{model.Name}.png");

            // Write the file content
            await File.WriteAllBytesAsync(fileName.FullName, model.ImageContent);

            // Modify the content of the pokemon
            PokemonFactory.Update(pokemon, model);


            // Save the data
            await _localStorage.SetItemAsync("data", currentData);
        }

        public async Task Delete(int id)
        {
            // Get the current data
            var currentData = await _localStorage.GetItemAsync<List<Pokemon>>("data");

            // Get the pokemon int the list
            var pokemon = currentData.FirstOrDefault(w => w.Id == id);

            // Delete pokemon in
            currentData.Remove(pokemon);

            // Delete the image
            var imagePathInfo = new DirectoryInfo($"{_webHostEnvironment.WebRootPath}/images");
            var fileName = new FileInfo($"{imagePathInfo}/{pokemon.Name}.png");

            if (fileName.Exists)
            {
                File.Delete(fileName.FullName);
            }

            // Save the data
            await _localStorage.SetItemAsync("data", currentData);
        }
    }


}
