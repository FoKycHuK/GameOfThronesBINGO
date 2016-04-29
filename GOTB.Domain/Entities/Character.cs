namespace GoTB.Domain.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsMan { get; set; }
        public int Age { get; set; }
        public bool IsAlive { get; set; }
        public int PopularityPoints { get; set; }
        public string ImageName { get; set; }
    }
}