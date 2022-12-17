using BlazorPokemon.Models;
using System.Xml.Linq;

namespace BlazorPokemon.Factories
{
    public static class PokemonFactory
    {
        public static PokemonModel ToModel(Pokemon pokemon) //byte[] imageContent)
        {
            return new PokemonModel
            {
                Id = pokemon.Id,
                DisplayName = pokemon.DisplayName,
                Name = pokemon.Name,
                HealthPoints = pokemon.HealthPoints,
                ElementType = pokemon.ElementType
                //ImageContent = imageContent
            };
        }

        public static Pokemon Create(PokemonModel model)
        {
            return new Pokemon
            {
                Id = model.Id,
                DisplayName = model.DisplayName,
                Name = model.Name,
                HealthPoints = model.HealthPoints,
                ElementType = model.ElementType,
                CreatedDate = DateTime.Now
            };
        }

        public static void Update(Pokemon pokemon, PokemonModel model)
        {
            pokemon.Id = model.Id;
            pokemon.DisplayName = model.DisplayName;
            pokemon.Name = model.Name;
            pokemon.HealthPoints = model.HealthPoints;
            pokemon.ElementType = model.ElementType;
            pokemon.UpdatedDate = DateTime.Now;
        }
    }
}
