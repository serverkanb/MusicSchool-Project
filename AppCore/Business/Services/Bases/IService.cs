using AppCore.Records.Bases;
using AppCore.Results.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Business.Services.Bases
{
	//new'lenebilen ve RecordBase'den miras alan tipler üzerinden işlemlerin yapılabileceği ve IDisposable interface'ini implemente edecek interface. 
	public interface IService<TModel> : IDisposable where TModel : RecordBase, new()
	{
		IQueryable<TModel> Query();
		Result Add(TModel model); 
		Result Update(TModel model); 
		Result Delete(int id); 
	}
}
