using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NDV_PetLoversClinicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NDV_PetLoversClinic") ?? throw new InvalidOperationException("Connection string 'NDV_PetLoversClinic' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//repository DI
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
