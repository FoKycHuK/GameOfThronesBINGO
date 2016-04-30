using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Concrete
{
    public class VoteDBRepository : IVoteRepository
    {
        private EFDbContext context;

        public VoteDBRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Vote> Votes { get { return context.Votes; } }

        public void Add(Vote vote)
        {
            context.Votes.Add(vote);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<Vote> votes)
        {
            context.Votes.AddRange(votes);
            context.SaveChanges();
        }
    }
}
