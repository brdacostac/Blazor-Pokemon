namespace BlazorPokemon.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public List<string> ElementType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
