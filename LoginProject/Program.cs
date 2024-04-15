using LoginProject.Data;
using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
});

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/Admin", "admin");
    options.Conventions.AuthorizeFolder("/Posts");
    options.Conventions.AuthorizePage("/Post");
});

// dotnet user-secrets set "Authentication__Google__ClientId" "" --project LoginProject
// dotnet user-secrets set "Authentication__Google__ClientSecret" "" --project LoginProject

builder.Services.AddAuthentication().AddGoogle(options => {
    options.ClientId = builder.Configuration["Authentication__Google__ClientId"];
    options.ClientSecret = builder.Configuration["Authentication__Google__ClientSecret"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
} else {
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
