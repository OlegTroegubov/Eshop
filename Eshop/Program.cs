using Eshop.Models;
using Eshop.Persistence.Configuration;
using Eshop.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Set up configuration
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build().GetConnectionString("DefaultConnection")));

//AddSingleton - создает один экземпляр на весь жизенный цикл программы
//AddScoped - создает один экземпляр на каждый http запрос
//AddTransient - создает экземпляр каждый раз когда он запрашивается
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddScoped<ProductCategoryService>();
var app = builder.Build();
using var scope = app.Services.CreateScope();
var dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
var productCategoryService = scope.ServiceProvider.GetRequiredService<ProductCategoryService>();
await dbSeeder.SeedAsync();
await productCategoryService.GetCategoriesAsync();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
