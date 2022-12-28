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
        public byte[] ImageContent { get; set; }
        public string ImageBase64 { get; set; }

        public static int compareType(Pokemon p1, Pokemon p2)
        {
            //if (p1.ElementType[0]=="Elektrik" && p2) { }
            return 0;
        }
    }
}
