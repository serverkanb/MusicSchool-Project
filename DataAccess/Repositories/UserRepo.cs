using AppCore.Data_Access.Entity_Framework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class UserRepoBase : RepoBase<User>
    {
        public UserRepoBase(MusicSchoolContext dbContext) : base(dbContext)
        {

        }
    }

    public class UserRepo : UserRepoBase
    {
        public UserRepo(MusicSchoolContext dbContext) : base(dbContext)
        {

        }

    }
}
