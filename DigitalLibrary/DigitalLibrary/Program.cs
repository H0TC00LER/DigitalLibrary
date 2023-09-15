using DigitalLibrary.Extencions;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Contracts;
using Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors(); //ext

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen(); //ext

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<AppDbContext>(
        options => options.UseNpgsql(
            connString, 
            b => b.MigrationsAssembly("DigitalLibrary")));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBookLoadingService>(x => new BookLoadingService(builder.Configuration));
builder.Services.AddScoped<IImageLoaderService>(x => new ImageLoaderService(builder.Configuration));
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddAuthentication();

builder.Services.ConfigureIdentity(); //ext
builder.Services.ConfigureJWT(builder.Configuration); //ext

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
