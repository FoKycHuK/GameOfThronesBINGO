using System.Linq;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Abstract
{
    public interface ICharacterRepository
    {
        IQueryable<Character> Characters { get; }
    }
}