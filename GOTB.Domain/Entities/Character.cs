using System.ComponentModel.DataAnnotations;

namespace GoTB.Domain.Entities
{
    public enum IsAlive
    {
        [Display(Name = "Жив")]
        Alive,
        [Display(Name = "Мертв")]
        Dead,
        [Display(Name = "Вроде бы жив")]
        IsProbablyAlive
    }

    public enum Gender
    {
        [Display(Name = "Мужской")]
        Male,
        [Display(Name = "Женский")]
        Female
    }

    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [UIHint("Enum")]
        public Gender Gender { get; set; }
        public int Age { get; set; }
        [UIHint("Enum")]
        public IsAlive IsAlive { get; set; }
        public int PopularityPoints { get; set; }
        public string ImageName { get; set; }
    }
}