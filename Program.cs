using KaiCryptoTracker.TokenService; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); 

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

app.UseHsts(); 
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();  

app.Run();
