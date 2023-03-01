namespace AppCore.Records.Bases
{

	//istenilen entitylerin veri tabanında tutularak,silindi olarak işaretlenmesini sağlayan sağladığımız sınıftır.
	//böylelikle istenirse bu kayıtlara tekrar ulaşılabilir.
	public interface ISoftDelete
	{
		bool IsDeleted { get; set; }
		

	}
}
