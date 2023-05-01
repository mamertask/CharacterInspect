using CharacterInspect.Services.Contracts;
using CharacterInspect.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IConfiguration>(config);

builder.Services.AddSingleton<IClientCredentials>(new BlizzardClientCredentials(config["BlizzardApi:ClientId"], config["BlizzardApi:ClientSecret"]));
builder.Services.AddScoped<IApiAuthService, BlizzardApiAuthService>();
builder.Services.AddScoped<IBlizzardApiService, BlizzardApiService>();
builder.Services.AddHttpClient();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
