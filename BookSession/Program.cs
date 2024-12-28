using BookSession.Services;
using Microsoft.EntityFrameworkCore;
using BookSession.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
      {
          options.UseSqlServer("Server=tcp:.database.windows.net,1433;Initial Catalog=BookSession_db;Persist Security Info=False;User ID=MotsinoniPhill;Password=@Mp0606255955;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
      }
    );

//Adding Idenetity services to the container
builder.Services.AddIdentity<ApplicationUser, IdentityRole>( options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
void var(DbContextOptionsBuilder builder)
{
    throw new NotImplementedException();
}

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


//Create the roles aned first admin user if not available
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
    var roleManageer = scope.ServiceProvider.GetRequiredService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

    await DatabaseInitializer.SendDateAsync(userManager!, roleManageer!);
}

app.Run();
