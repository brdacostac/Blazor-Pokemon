using BlazorPokemon.Factories;
using BlazorPokemon.Models;
using Microsoft.AspNetCore.Components;

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
        // Get the Pokemon
        var pokemon = PokemonFactory.Create(model);
        
        // Save the data
        await _http.PostAsJsonAsync("https://localhost:7234/api/Crafting/", pokemon);
    }

    public async Task<int> Count()
    {
        return await _http.GetFromJsonAsync<int>("https://localhost:7234/api/Crafting/count");
    }

    public async Task<List<Pokemon>> List(int currentPage, int pageSize)
    {
        return await _http.GetFromJsonAsync<List<Pokemon>>($"https://localhost:7234/api/Crafting/?currentPage={currentPage}&pageSize={pageSize}");
    }
    
    public async Task<Pokemon> GetById(int id)
    {
        return await _http.GetFromJsonAsync<Pokemon>($"https://localhost:7234/api/Crafting/{id}");
    }

    public async Task Update(int id, PokemonModel model)
    {
        // Get the Pokemon
        var pokemon = PokemonFactory.Create(model);
        
        await _http.PutAsJsonAsync($"https://localhost:7234/api/Crafting/{id}", pokemon);
    }

    public async Task UpdateLoser(int id, Pokemon pokemon)
    {

        await _http.PutAsJsonAsync($"https://localhost:7234/api/Crafting/{id}", pokemon);
    }

        public async Task Delete(int id)
    {
        await _http.DeleteAsync($"https://localhost:7234/api/Crafting/{id}");
    }

        public async Task<List<Pokemon>> All()
        {
            return await _http.GetFromJsonAsync<List<Pokemon>>($"https://localhost:7234/api/Crafting/all");
        }


}
}
