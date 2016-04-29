using System.Linq;
using GoTB.Domain.Abstract;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Concrete
{
    public class EfCharacterRepository : ICharacterRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Character> Characters
        {
            get { return context.Characters; }
        }
    }
}