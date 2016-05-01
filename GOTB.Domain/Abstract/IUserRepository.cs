using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<UserProfile> UserProfiles { get; }
    }
}
