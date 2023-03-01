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
    public abstract class CityRepoBase : RepoBase<City>
    {
        protected CityRepoBase(MusicSchoolContext dbContext) : base(dbContext)
        {
        }
    }

    public class CityRepo : CityRepoBase
    {
        public CityRepo(MusicSchoolContext dbContext) : base(dbContext)
        {
        }
    }
}

