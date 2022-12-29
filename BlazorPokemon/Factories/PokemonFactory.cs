using BlazorPokemon.Models;
using System.Xml.Linq;

namespace BlazorPokemon.Factories
{
    public static class PokemonFactory
    {
        public static PokemonModel ToModel(Pokemon pokemon, byte[] imageContent)
        {
            return new PokemonModel
            {
                Id = pokemon.Id,
                DisplayName = pokemon.DisplayName,
                Name = pokemon.Name,
                HealthPoints = pokemon.HealthPoints,
                PointsAttack = pokemon.PointsAttack,
                PointsDefense = pokemon.PointsDefense,
                ElementType = pokemon.ElementType,
                ImageContent = pokemon.ImageContent,
                ImageBase64 = string.IsNullOrWhiteSpace(pokemon.ImageBase64) ? Convert.ToBase64String(pokemon.ImageContent) : pokemon.ImageBase64
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
                PointsAttack = model.PointsAttack,
                PointsDefense = model.PointsDefense,
                ElementType = model.ElementType,
                CreatedDate = DateTime.Now,
                ImageBase64 = Convert.ToBase64String(model.ImageContent)
            };
        }

        public static void Update(Pokemon pokemon, PokemonModel model)
        {
            pokemon.Id = model.Id;
            pokemon.DisplayName = model.DisplayName;
            pokemon.Name = model.Name;
            pokemon.HealthPoints = model.HealthPoints;
            pokemon.PointsAttack = model.PointsAttack;
            pokemon.PointsDefense = model.PointsDefense;
            pokemon.ElementType = model.ElementType;
            pokemon.UpdatedDate = DateTime.Now;
            pokemon.ImageBase64 = Convert.ToBase64String(model.ImageContent);
        }
    }
}
