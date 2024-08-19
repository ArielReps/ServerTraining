using FirstWebApp.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string ConnectionString = "server=ARIELOHANAPC\\SQLEXPRESS;initial catalog=ServerTraining; user id=sa; password=1234; TrustServerCertificate=Yes;";
builder.Services.AddDbContext<DataLayer>(options => options.UseSqlServer(ConnectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
