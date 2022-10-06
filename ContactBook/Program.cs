using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ContactBook.Data;
using Microsoft.EntityFrameworkCore.Design;
using ContactBook.Models;
using ContactBook.Services;
using ContactBook.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using ContactBook.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//connection string to our database
//var connectionString = builder.Configuration.GetConnectionString("Default");
//var connectionString = builder.Configuration.GetSection("pgSettings")["pgConnection"];
var connectionString = ConnectionHelper.GetConnectionString(builder.Configuration);

//configured to use postgres driver 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddRazorPages();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//custom services
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAddressBookService, AddressBookService>();
builder.Services.AddScoped<IEmailSender, EmailService>();

//configure email service
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

//builds the app
var app = builder.Build();
//grabs our services
var scope = app.Services.CreateScope();
//gets db update with the latest migrations
await DataHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//custom page route for errors
app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

