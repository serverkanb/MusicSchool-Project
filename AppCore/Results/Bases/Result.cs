using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Results.Bases
{
	public abstract class Result 
	{
		//burada issuccesfullun atamasını constructorlar üzerinden yapacağımız için set'i kaldırarak  readonly yaptık.
		public bool IsSuccessful { get; set; } 
		public string Message { get; set; }

		public Result(bool isSuccessful, string message)
		{
			IsSuccessful = isSuccessful;
			Message = message;
		}
	}
}
