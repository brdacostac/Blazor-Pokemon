namespace BlazorPokemon.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int PointsAttack { get; set; }
        public int PointsDefense { get; set; }
        public List<string> ElementType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImageBase64 { get; set; }

        public static int compareType(Pokemon p1, Pokemon p2)
        {
           
            if (p1.getPower() > p2.getPower())  {
                
                if(p2.PointsDefense > p1.PointsAttack){ // malgrès que P1 gagne. P2 a plus de defense que P1 a d'attaque. P2 recois donc 50% de l'attaque de p1.
                    
                    p2.HealthPoints = p2.HealthPoints - (int)(p1.PointsAttack*0.5);
                    if (p2.HealthPoints <= 0)
                    {
                        p2.HealthPoints = 0;
                    }
                    return 0;
                }
                
                p2.HealthPoints = p2.HealthPoints + p2.PointsDefense - p1.PointsAttack;
                if (p2.HealthPoints <= 0)
                {
                    p2.HealthPoints = 0;
                }
                return 0;
            }
            else{
                if(p1.PointsDefense > p2.PointsAttack) {    // malgrès que P2 gagne. P1 a plus de defense que P2 a d'attaque. P1 recois donc 50% de l'attaque de p2.
                    p1.HealthPoints = p1.HealthPoints - (int)(p2.PointsAttack*0.5);
                    if (p1.HealthPoints <= 0)
                    {
                        p1.HealthPoints = 0;
                    }
                    return 1;
                }
                    
                p1.HealthPoints = p1.HealthPoints + p1.PointsDefense - p2.PointsAttack;
                if (p1.HealthPoints <= 0)
                {
                    p1.HealthPoints = 0;
                }
                return 1;
            }
 
        }
        public int getPower()
        {
            return (PointsAttack + PointsDefense + HealthPoints);
        }
    }
}
