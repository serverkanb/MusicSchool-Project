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
    public abstract class CountryRepoBase : RepoBase<Country>
    {
        protected CountryRepoBase(MusicSchoolContext dbContext) : base(dbContext)
        {
        }
    }

    public class CountryRepo : CountryRepoBase
    {
        public CountryRepo(MusicSchoolContext dbContext) : base(dbContext)
        {
        }
    }
}
