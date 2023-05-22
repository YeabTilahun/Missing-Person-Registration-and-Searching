using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Missing_Person.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<MissingPersonDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("MissingPersonContextConnection")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{ 
options.Password.RequiredLength = 8;
options.Password.RequiredUniqueChars = 3;
}).AddEntityFrameworkStores<MissingPersonDbContext>();

//Authorization
/*
builder.Services.AddMvc(options =>
{
var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
options.Filters.Add(new AuthorizeFilter(policy));
}).AddXmlSerializerFormatters();*/

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
