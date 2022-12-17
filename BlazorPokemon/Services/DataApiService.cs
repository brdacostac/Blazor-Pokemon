using BlazorPokemon.Factories;
using BlazorPokemon.Models;

namespace BlazorPokemon.Services
{
    public class DataApiService : IDataService
    {
        private readonly HttpClient _http;

        public DataApiService(
            HttpClient http)
        {
            _http = http;
        }

        public async Task Add(PokemonModel model)
        {
            // Get the item
            var pokemon = PokemonFactory.Create(model);

            // Save the data
            await _http.PostAsJsonAsync("https://localhost:7234/api/Crafting/count", pokemon);
        }

        public async Task<int> Count()
        {
            return await _http.GetFromJsonAsync<int>("https://localhost:7234/api/Crafting/count");
        }

        public async Task<List<Pokemon>> List(int currentPage, int pageSize)
        {
            var pokemonJson = await _http.GetFromJsonAsync<List<Pokemon>>($"https://pokeapi.co/api/v2/pokemon?limit=15&offset=0");
            return pokemonJson.ToList();
        }

        public async Task<Pokemon> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Pokemon>($"https://localhost:7234/api/Crafting/{id}");
        }

        public async Task Update(int id, PokemonModel model)
        {
            // Get the item
            var item = PokemonFactory.Create(model);

            await _http.PutAsJsonAsync($"https://localhost:7234/api/Crafting/{id}", item);
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"https://localhost:7234/api/Crafting/{id}");
        }

    }
}
