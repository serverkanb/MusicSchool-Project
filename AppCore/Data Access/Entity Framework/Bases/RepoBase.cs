using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Data_Access.Entity_Framework.Bases
{
	//Repobase classı DbContext üzerinden Entity'leri kullanarak Crud işlerimlerini gerçekleştirdiğimiz class'ımızdır.
	public abstract class RepoBase<TEntity> : IDisposable where TEntity : RecordBase, new()
	{
		//Dbcontext'i constructor üzerinden injection yaparak DbContext bağlantısını kuruyoruz.
		protected readonly DbContext _dbContext;

		protected RepoBase(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		//ilgili Entity için sorguyu oluşturan methoddur.
		//Virtual tanımlayarak daha sonrasında özelleştirilmesi sağlanmakta
		//Params ile bağlı tablolar sorguya dahil edilir.
		//Func ile delege kullanarak sorgular oluşturulur. 
		public virtual IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] entitiesToInclude)
		{
			var query = _dbContext.Set<TEntity>().AsQueryable();
			foreach (var entityToInclude in entitiesToInclude)
			{
				query = query.Include(entityToInclude);
			}
			return query;
		}
		//burada Query methodunu predicate ile bool sonuç dönen where ile filtreleyen overload methoddur.
		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] entitiesToInclude)
		{
			return Query(entitiesToInclude).Where(predicate);
		}

		// Raporlama yapabilmek için ilişkili entitileri kullanan sorgudur. 
		public virtual IQueryable<TRelationalEntity> Query<TRelationalEntity>() where TRelationalEntity : class, new()
		{
			return _dbContext.Set<TRelationalEntity>().AsQueryable();
		}
		//Kayıt ekleme için kullanıp veri tabanına yansıtacağımız methoddur.
		public virtual void Add(TEntity entity, bool save = true)
		{
			_dbContext.Set<TEntity>().Add(entity);
			if (save)
				Save();
		}
		//Predicate ile belirtilen koşul ve ya koşullara uygun kayıt olup olmadığını sorgulayan methoddur.
		public virtual bool Exists(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] entitiesToInclude)
		{
			return Query(entitiesToInclude).Any(predicate);
		}
		//DbSet üzerinden  gönderilen entity'i Güncelleme için kullandığımız ve Save  ile veri tabanına yansıttığımız methoddur.
		public virtual void Update(TEntity entity, bool save = true)
		{
			_dbContext.Set<TEntity>().Update(entity);
			if (save)
				Save();
		}
		//DbSet üzerinden  gönderilen entity'i çıkarmak için kullandığımız ve Save  ile veri tabanına yansıttığımız methoddur.
		public virtual void Delete(TEntity entity, bool save = true)
		{
			_dbContext.Set<TEntity>().Remove(entity);
			if (save)
				Save();
		}

		//ilişkili verileri  silmek için kullandığımız methoddur.
		public virtual void Delete<TRelationalEntity>(Expression<Func<TRelationalEntity, bool>> predicate, bool save = false)
			where TRelationalEntity : class, new()
		{
			var relationalEntities = _dbContext.Set<TRelationalEntity>().Where(predicate).ToList();
			_dbContext.Set<TRelationalEntity>().RemoveRange(relationalEntities);
			if (save) Save();
		}
		//id üzerinden verileri silmek için kullandığımız methoddur.
		public virtual void Delete(int id, bool save = true)
		{
			var entity = _dbContext.Set<TEntity>().SingleOrDefault(e => e.Id == id);
			Delete(entity, save);
		}
		//birden çok kaydı silip,tek seferde veri tabanına yansıttığımız methoddur.
		public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
		{
			var entities = Query(predicate).ToList();
			foreach (var entity in entities)
			{
				Delete(entity, false);
			}
			if (save)
				Save();
		}
		//Ali için içini doldurduğumuz methoddur :)
		//Yapılan değişiklikleri veri tabanına yansıttığımız methoddur.
		public virtual int Save()
		{
			try
			{
				return _dbContext.SaveChanges();
			}
			catch (Exception exc)
			{
				// exc üzerinden loglama
				throw exc;
			}
		}

		//Garbage collectora işimizin bittiğini haber verip, hafızadan temizleyen methoddur. 

		public void Dispose()
		{
			_dbContext?.Dispose();
			GC.SuppressFinalize(this);
		}
	}

}
