using AppCore.Data_Access.Entity_Framework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public abstract class StudentRepoBase : RepoBase<Student>
	{
		protected StudentRepoBase(MusicSchoolContext dbContext) : base(dbContext)
		{
		}
	}
	public class StudentRepo : StudentRepoBase
	{
		public StudentRepo(MusicSchoolContext dbContext) : base(dbContext)
		{
		}
	}
}
