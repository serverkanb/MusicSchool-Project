using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
#region Localization
// Web uygulamasýnýn bölgesel ayarý aþaðýdaki þekilde tek seferde konfigüre edilerek tüm projenin bu ayarý kullanmasý saðlanabilir,
// dolayýsýyla veri formatlama veya dönüþtürme gibi iþlemlerde her seferinde CultureInfo objesinin kullaným gereksinimi ortadan kalkar.
// Bu þekilde sadece tek bir bölgesel ayar projede kullanýlabilir.
List<CultureInfo> cultures = new List<CultureInfo>()
{
	new CultureInfo("en-US") // eðer uygulama Türkçe olacaksa CultureInfo constructor'ýnýn parametresini ("tr-TR") yapmak yeterlidir.
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
	options.SupportedCultures = cultures;
	options.SupportedUICultures = cultures;
});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Account/Users/Login";
        config.AccessDeniedPath = "/Account/Users/AccessDenied";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });
#endregion

#region Session
builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(40); //default 20 dk
    config.IOTimeout = Timeout.InfiniteTimeSpan;
});
#endregion

#region AppSettings
//var section = builder.Configuration.GetSection("AppSettings");
var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());
#endregion



#region IoC Container (Inversion of Control)
//Autofac,Ninject baðýmlýlýklarýn yönetilmesini saðlayan kütüphaneler
var connectionString = builder.Configuration.GetConnectionString("MusicSchoolDB");
builder.Services.AddDbContext<MusicSchoolContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ClassroomRepoBase, ClassroomRepo>(); // istek bazlý
//builder.Services.AddScoped<ContactRepoBase, ContactRepo>();
builder.Services.AddScoped<LessonRepoBase, LessonRepo>();
builder.Services.AddScoped<StudentRepoBase, StudentRepo>();
builder.Services.AddScoped<TeacherRepoBase, TeacherRepo>();
builder.Services.AddScoped<InstrumentRepoBase, InstrumentRepo>();
builder.Services.AddScoped<CityRepoBase, CityRepo>();
builder.Services.AddScoped<CountryRepoBase, CountryRepo>();
builder.Services.AddScoped<UserRepoBase, UserRepo>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IInstrumentService, InstrumentService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICountryService, CountryService>();

#endregion

var app = builder.Build();
#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
	DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
	SupportedCultures = cultures,
	SupportedUICultures = cultures,
});
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region Session
app.UseSession();
#endregion

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
