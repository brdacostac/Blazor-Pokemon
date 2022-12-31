namespace Pokemon.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pokemon.Api;
    using Pokemon.Api.Models;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The crafting controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        /// <summary>
        /// The json serializer options.
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        /// <summary>
        /// Adds the specified pokemon.
        /// </summary>
        /// <param name="pokemon">The pokemon.</param>
        /// <returns>The async task.</returns>
        [HttpPost]
        [Route("")]
        public Task Add(Pokemon pokemon)
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            data.Add(pokemon);

            System.IO.File.WriteAllText("Data/pokemons.json", JsonSerializer.Serialize(data, _jsonSerializerOptions));

            return Task.CompletedTask;
        }


        [HttpGet]
        [Route("all")]
        public Task<List<Pokemon>> All()
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            return Task.FromResult(data.ToList());
        }

        /// <summary>
        /// Count the number of pokemons.
        /// </summary>
        /// <returns>The number of pokemons.</returns>
        [HttpGet]
        [Route("count")]
        public Task<int> Count()
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            return Task.FromResult(data.Count);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The async task.</returns>
        [HttpDelete]
        [Route("{id}")]
        public Task Delete(int id)
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            var pokemon = data.FirstOrDefault(w => w.Id == id);

            if (pokemon == null)
            {
                throw new Exception($"Unable to found the pokemon with ID: {id}");
            }

            data.Remove(pokemon);

            System.IO.File.WriteAllText("Data/pokemons.json", JsonSerializer.Serialize(data, _jsonSerializerOptions));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the pokemon by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The pokemon.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<Pokemon> GetById(int id)
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            var pokemon = data.FirstOrDefault(w => w.Id == id);

            if (pokemon == null)
            {
                throw new Exception($"Unable to found the pokemon with ID: {id}");
            }

            return Task.FromResult(pokemon);
        }

        /// <summary>
        /// Gets the pokemon by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The pokemon.
        /// </returns>
        [HttpGet]
        [Route("by-name/{name}")]
        public Task<Pokemon> GetByName(string name)
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            var pokemon = data.FirstOrDefault(w => w.Name.ToLowerInvariant() == name.ToLowerInvariant());

            if (pokemon == null)
            {
                throw new Exception($"Unable to found the pokemon with name: {name}");
            }

            return Task.FromResult(pokemon);
        }


        /// <summary>
        /// Get the pokemons with pagination.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The pokemons.</returns>
        [HttpGet]
        [Route("")]
        public Task<List<Pokemon>> List(int currentPage, int pageSize)
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            return Task.FromResult(data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Resets the pokemons.
        /// </summary>
        /// <returns>The async task.</returns>
        [HttpGet]
        [Route("reset-pokemons")]
        public Task ResetPokemons()
        {
            if (!System.IO.File.Exists("Data/pokemons.json"))
            {
                System.IO.File.Delete("Data/pokemons.json");
            }

            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons-original.json"), _jsonSerializerOptions);

            if (data == null)
            {
                throw new Exception("Unable to get the pokemons.");
            }

            var defaultImage = Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/default.png"));

            var imageTranslation = new Dictionary<string, string>
            {
                { "stone_slab", "smooth_stone_slab_side" },
                { "sticky_piston", "piston_top_sticky" },
                { "mob_spawner", "spawner" },
                { "chest", "chest_minecart" },
                { "stone_stairs", "stairs" },
            };

            foreach (var pokemon in data)
            {
                var imageFilepath = defaultImage;

                if (System.IO.File.Exists($"Images/{pokemon.Name}.png"))
                {
                    imageFilepath = Convert.ToBase64String(System.IO.File.ReadAllBytes($"Images/{pokemon.Name}.png"));
                }

                if (imageFilepath == defaultImage && System.IO.File.Exists($"Images/{pokemon.Name}_top.png"))
                {
                    imageFilepath = Convert.ToBase64String(System.IO.File.ReadAllBytes($"Images/{pokemon.Name}_top.png"));
                }

                if (imageFilepath == defaultImage && System.IO.File.Exists($"Images/{pokemon.Name}_front.png"))
                {
                    imageFilepath = Convert.ToBase64String(System.IO.File.ReadAllBytes($"Images/{pokemon.Name}_front.png"));
                }

                if (imageFilepath == defaultImage && System.IO.File.Exists($"Images/white_{pokemon.Name}.png"))
                {
                    imageFilepath = Convert.ToBase64String(System.IO.File.ReadAllBytes($"Images/white_{pokemon.Name}.png"));
                }

                if (imageFilepath == defaultImage && System.IO.File.Exists($"Images/oak_{pokemon.Name}.png"))
                {
                    imageFilepath = Convert.ToBase64String(System.IO.File.ReadAllBytes($"Images/oak_{pokemon.Name}.png"));
                }

                if (imageFilepath == defaultImage && System.IO.File.Exists($"Images/{pokemon.DisplayName.ToLower().Replace(" ", "_")}.png"))
                {
                    imageFilepath = Convert.ToBase64String(System.IO.File.ReadAllBytes($"Images/{pokemon.DisplayName.ToLower().Replace(" ", "_")}.png"));
                }

                if (imageFilepath == defaultImage && imageTranslation.ContainsKey(pokemon.Name))
                {
                    imageFilepath = Convert.ToBase64String(System.IO.File.ReadAllBytes($"Images/{imageTranslation[pokemon.Name]}.png"));
                }

                pokemon.ImageBase64 = imageFilepath;
            }

            System.IO.File.WriteAllText("Data/pokemons.json", JsonSerializer.Serialize(data, _jsonSerializerOptions));

            return Task.FromResult(data);
        }


        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="pokemon">The pokemon.</param>
        /// <returns>The async task.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task Update(int id, Pokemon pokemon)
        {
            var data = JsonSerializer.Deserialize<List<Pokemon>>(System.IO.File.ReadAllText("Data/pokemons.json"), _jsonSerializerOptions);

            var pokemonOriginal = data?.FirstOrDefault(w => w.Id == id);

            if (pokemonOriginal == null)
            {
                throw new Exception($"Unable to found the pokemon with ID: {id}");
            }

            pokemonOriginal.Id = pokemon.Id;
            pokemonOriginal.Name = pokemon.Name;
            pokemonOriginal.HealthPoints = pokemon.HealthPoints;
            pokemonOriginal.CreatedDate = pokemon.CreatedDate;
            pokemonOriginal.DisplayName = pokemon.DisplayName;
            pokemonOriginal.ElementType = pokemon.ElementType;
            pokemonOriginal.UpdatedDate = pokemon.UpdatedDate;
            pokemonOriginal.ImageBase64 = pokemon.ImageBase64;

            System.IO.File.WriteAllText("Data/pokemons.json", JsonSerializer.Serialize(data, _jsonSerializerOptions));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the name of the pokemon.
        /// </summary>
        /// <param name="pokemons">The pokemons.</param>
        /// <param name="inShape">The in shape.</param>
        /// <param name="line">The line.</param>
        /// <param name="row">The row.</param>
        /// <returns>The name of the pokemon.</returns>
        private static string GetPokemonName(List<Pokemon> pokemons, InShape[][] inShape, int line, int row)
        {
            if (inShape.Length < line + 1)
            {
                return null;
            }

            if (inShape[line].Length < row + 1)
            {
                return null;
            }

            var id = inShape[line][row].Integer ?? inShape[line][row].IngredientClass?.Id;

            if (id == null)
            {
                return null;
            }

            return GetPokemonName(pokemons, id.Value);
        }

        /// <summary>
        /// Gets the name of the pokemon.
        /// </summary>
        /// <param name="pokemons">The pokemons.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>The name of the pokemon.</returns>
        private static string GetPokemonName(List<Pokemon> pokemons, long id)
        {
            var pokemon = pokemons.FirstOrDefault(w => w.Id == id);
            return pokemon?.Name;
        }

    }
}