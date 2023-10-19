using CAVBackEndUpdate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CAVBackEndUpdate.Services;
using CAVBackEndUpdate.Reopsitory;
using CAVBackEndUpdate.Utils;

var builder = WebApplication.CreateBuilder(args);


// Add services to the cont;ainer.
builder.Services.AddEntityFrameworkSqlServer();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_Name"); ;
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True;MultipleActiveResultSets=true;integrated security=false;";
builder.Services.AddDbContextPool<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
//$"Server={dbHost};Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False User ID=sa;Password={dbPassword}"

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddHostedService<BackGroundTask>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
