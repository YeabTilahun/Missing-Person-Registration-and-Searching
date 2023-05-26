using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Missing_Person.Controllers;
using Missing_Person.Models;
using Missing_Person.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Services for identity and database
builder.Services.AddDbContextPool<MissingPersonDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("MissingPersonContextConnection")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{ 
options.Password.RequiredLength = 8;
options.Password.RequiredUniqueChars = 3;
}).AddEntityFrameworkStores<MissingPersonDbContext>();

//dependency injection
builder.Services.AddScoped<IMissingPersonRepository, MissingPersonRepository>();


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
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
