using HKNGroup.Persistence.Contexts;
using HKNGroup.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");

builder.Services.AddDbContext<HKNGroupContext>(opts =>
    opts.UseSqlServer(connectionString, options =>
        options.MigrationsAssembly("HKNGroup.Persistence")
               .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "dbo")));

builder.Services.AddAutoMapper(options => options.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"], typeof(EntityProfile));
var mvcBuilder = builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value => $"'{value}' değeri geçersizdir.");
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan boş bırakılamaz.");
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, field) => $"'{field}' alanı için girilen değer geçersizdir.");
    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Bir değer girilmelidir.");
    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(field => $"'{field}' alanı için bir değer girilmelidir.");
    options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "İstek içeriği boş olamaz.");
    options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(value => "Girilen değer geçersizdir.");
    options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "Girilen değer geçersizdir.");
    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "Bu alan sayısal bir değer olmalıdır.");
    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(field => $"'{field}' alanı için girilen değer geçersizdir.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(field => $"'{field}' alanı sayısal bir değer olmalıdır.");
});
#if DEBUG
mvcBuilder.AddRazorRuntimeCompilation();
#endif
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.LoadMyService(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(10);
});
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt => { opt.LoginPath = "/Admin/Login"; });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Admin"), builder =>
{
    builder.UseMiddleware<HtmlManipulationMiddleware>();
});




app.MapAreaControllerRoute(
    name: "admin_login",
    areaName: "Admin",
    pattern: "Admin/Login",
    defaults: new { controller = "Auth", action = "Login" });

app.MapAreaControllerRoute(
    name: "admin_logout",
    areaName: "Admin",
    pattern: "Admin/Logout",
    defaults: new { controller = "Auth", action = "Logout" });

app.MapAreaControllerRoute(
    name: "admin_default",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
          name: "default",
          pattern: "/",
          defaults: new { controller = "Home", action = "Index" }
    );
app.MapControllerRoute(
     name: "default",
     pattern: "anasayfa",
     defaults: new { controller = "Home", action = "Index" }
);

app.MapControllerRoute(
     name: "Referances",
     pattern: RoutingEnum.Referance + "/{name?}/{id?}",
     defaults: new { controller = "Referances", action = "Index" }
);

app.MapControllerRoute(
     name: "Article",
     pattern: RoutingEnum.Haberler,
     defaults: new { controller = "Article", action = "Index" }
);



app.MapControllerRoute(
    name: "Article",
    pattern: RoutingEnum.HaberDetay + "/{name?}/{id?}",
    defaults: new { controller = "Article", action = "Detail" }
);


app.MapControllerRoute(
    name: "Contents",
    pattern: RoutingEnum.Icerik + "/{name?}/{id?}",
    defaults: new { controller = "Contents", action = "Index" }
);

app.MapControllerRoute(
    name: "Project",
    pattern: RoutingEnum.Projeler + "/{name?}/{id?}",
    defaults: new { controller = "Project", action = "Index" }
);

app.MapControllerRoute(
   name: "Project",
   pattern: RoutingEnum.Proje + "/{name?}/{id?}",
   defaults: new { controller = "Project", action = "Detail" }
);
app.MapControllerRoute(
    name: "ProjectCategory",
    pattern: RoutingEnum.ProjeKategori + "/{name?}/{id?}",
    defaults: new { controller = "ProjectCategory", action = "Index" }
);

app.MapControllerRoute(
    name: "Services",
    pattern: RoutingEnum.Hizmetler + "/",
    defaults: new { controller = "Services", action = "Index" }
);

app.MapControllerRoute(
  name: "Services",
  pattern: RoutingEnum.Hizmet + "/{name?}",
  defaults: new { controller = "Services", action = "Details" }
);

app.MapControllerRoute(
  name: "Contact",
  pattern: RoutingEnum.Iletisim + "/{id?}",
  defaults: new { controller = "Contact", action = "Index" }
);

app.MapControllerRoute(
        name: "sitemap",
        pattern: "sitemap.xml",
        defaults: new { controller = "Sitemap", action = "Index" });


app.Run();
