using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class MusicSchoolContextFactory : IDesignTimeDbContextFactory<MusicSchoolContext> // ETradeContext objesini oluşturup kullanılmasını sağlayan fabrika class'ı,
                                                                                   // scaffolding işlemleri için bu class oluşturulmalıdır
    {
        public MusicSchoolContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicSchoolContext>();
            optionsBuilder.UseSqlServer("server=LAPTOP-1IHFPICF;database=musicschool45;trusted_connection=true;multipleactiveresultsets=true;trustservercertificate=true;");
            // önce veritabanımızın (development veritabanı kullanılması daha uygundur) connection string'ini içeren bir obje oluşturuyoruz

            return new MusicSchoolContext(optionsBuilder.Options); // daha sonra yukarıda oluşturduğumuz obje üzerinden ETradeContext tipinde bir obje dönüyoruz
        }
    }
}
