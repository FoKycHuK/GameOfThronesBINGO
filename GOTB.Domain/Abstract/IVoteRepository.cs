using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Concrete
{
    public interface IVoteRepository
    {
        IQueryable<Vote> Votes { get; }
        void Add(Vote vote);
        void AddRange(IEnumerable<Vote> votes);
    }
}
