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
        public IQueryable<VoteItem> VoteItems { get { return context.VoteItems; } }
    }
}
