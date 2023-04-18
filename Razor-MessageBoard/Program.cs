using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Razor_MessageBoard.Data;
using Razor_MessageBoard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


var authDbConnectionString = builder.Configuration.GetConnectionString("AuthDbConnection");
var messagesDbConnectionString = builder.Configuration.GetConnectionString("MessagesDbConnection");

builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authDbConnectionString));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(messagesDbConnectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";
});

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/Member");
});

builder.Services.AddScoped<MessagesRepo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
