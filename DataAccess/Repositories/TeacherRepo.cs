using AppCore.Data_Access.Entity_Framework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public abstract class TeacherRepoBase : RepoBase<Teacher>
	{
		protected TeacherRepoBase(MusicSchoolContext dbContext) : base(dbContext)
		{
		}
		public override IQueryable<Teacher> Query(params Expression<Func<Teacher, object>>[] entitiesToInclude) //sorguya dahil etmke
																							//istediklerimizi aşağıya gönderdik
		{
			return base.Query(entitiesToInclude).Where(q=>!q.IsDeleted); // kayıt getirme işkemi yaptığında sana silinmemişleri getircek
		}
		public override void Delete(Teacher entity, bool save = true)
		{
			entity.IsDeleted= true;
			base.Update(entity,save); //bir öğretmen sildiğinde kayıt silinmeyecek update edilcek.
		}
	}
	public class TeacherRepo : TeacherRepoBase
	{
		public TeacherRepo(MusicSchoolContext dbContext) : base(dbContext)
		{
		}
	}
}
