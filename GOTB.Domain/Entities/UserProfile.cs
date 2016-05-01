using System.ComponentModel.DataAnnotations;

namespace GoTB.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
