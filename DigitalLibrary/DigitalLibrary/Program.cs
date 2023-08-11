using DigitalLibrary.Extencions;
using Microsoft.EntityFrameworkCore;
using Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors(); //ext

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddDbContext<AppDbContext>(options => options.UseSqlServer(connString, b => b.MigrationsAssembly("DigitalLibrary")));

builder.Services.AddScoped<Service.Contracts.IAuthenticationService, Service.Implementations.AuthenticationService>();

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
