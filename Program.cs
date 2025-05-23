using KaiCryptoTracker.TokenService;
using KaiCryptoTracker.Identity;
using KaiCryptoTracker.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
var configuration = builder.Configuration; 

builder.Services.AddScoped<ITokenService, TokenService>();



//Add Identity to service
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders() //used to generate tokens like (password reset, etc)
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

//Add Db context to service and connect to db 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]); 
});




var app = builder.Build();


//create db if it does not exist 
using (var scoped = app.Services.CreateScope())
{
    var dbcontext = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbcontext.Database.MigrateAsync();

}
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
else
{
    app.UseHsts();
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();  

app.Run();
