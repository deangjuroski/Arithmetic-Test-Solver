using Microsoft.EntityFrameworkCore;
using StudentMathTestSystem.Interfaces;
using StudentMathTestSystem.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging());

builder.Services.AddScoped<IXmlValidationService, XmlValidationService>();
builder.Services.AddScoped<IXmlHelper, XmlHelper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teacher}/{action=Index}/{id?}");

app.Run();
