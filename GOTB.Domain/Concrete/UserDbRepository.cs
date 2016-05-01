using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTB.Domain.Abstract;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Concrete
{
    public class UserDbRepository : IUserRepository
    {
        EFDbContext context = new EFDbContext();

        public IQueryable<UserProfile> UserProfiles { get { return context.UserProfiles; } }
    }
}
