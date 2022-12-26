using System.ComponentModel.DataAnnotations;

namespace BlazorPokemon.Models
{
    public class PokemonModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Le nom affiché ne doit pas dépasser 50 caractères.")]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Le nom ne doit pas dépasser 50 caractères.")]
        [RegularExpression(@"^[a-z''-'\s]{1,40}$", ErrorMessage = "Seulement les caractères en minuscule sont acceptées.")]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int HealthPoints { get; set; }

        //[Required(ErrorMessage = "Le type du pokemon est obligatoire")]
        public List<string> ElementType { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vous devez accepter les conditions.")]
        public bool AcceptCondition { get; set; }

        [Required(ErrorMessage = "L'image du pokemon est obligatoire !")]
        public byte[] ImageContent { get; set; }

        public string ImageBase64 { get; set; }

    }
}
