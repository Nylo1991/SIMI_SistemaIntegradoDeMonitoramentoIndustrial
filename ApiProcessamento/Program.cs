using ApiProcessamento.Config;
using ApiProcessamento.Data;
using Microsoft.EntityFrameworkCore;

// 1. A criação do builder DEVE ser a primeira coisa após os 'usings'
var builder = WebApplication.CreateBuilder(args);

// Banco SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=simi.db"));

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config
builder.Services.Configure<ApiConfig>(
    builder.Configuration.GetSection("ApiConfig"));

// 2. O 'Build' encerra a configuração do builder e inicia o app
var app = builder.Build();

// 3. Daqui para baixo usamos o 'app'
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();