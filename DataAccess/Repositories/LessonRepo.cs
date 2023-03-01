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
	public abstract class LessonRepoBase : RepoBase<Lesson>
	{
		public LessonRepoBase(MusicSchoolContext dbContext) : base(dbContext)
		{
		}
	}

	public class LessonRepo : LessonRepoBase
	{
		public LessonRepo(MusicSchoolContext dbContext) : base(dbContext)
		{
		}
	}

}
