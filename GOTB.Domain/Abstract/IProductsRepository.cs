using System.Linq;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Character> Products { get; }
    }
}