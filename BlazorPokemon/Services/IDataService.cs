using BlazorPokemon.Models;

namespace BlazorPokemon.Services
{
    public interface IDataService
    {
        Task Add(PokemonModel model);

        Task<int> Count();

        Task<List<Pokemon>> List(int currentPage, int pageSize);

        Task<Pokemon> GetById(int id);

        Task Update(int id, PokemonModel model);

        Task Delete(int id);

        Task<List<Pokemon>> All();

    }
}
