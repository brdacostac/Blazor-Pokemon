namespace Pokemon.Api.Models
{

    public class Pokemon
    {

        public Pokemon()
        {
            ElementType = new List<string>();
        }
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int PointsAttack { get; set; }
        public int PointsDefense { get; set; }
        public List<string> ElementType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ImageBase64 { get; set; }
    }
}