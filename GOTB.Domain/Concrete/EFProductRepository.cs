using System.Linq;
using GoTB.Domain.Abstract;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Character> Products
        {
            get { return context.Products; }
        }
    }
}