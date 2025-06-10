using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using Repositories;
using Controllers;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlite("Data Source=library.db"));


builder.Services.AddScoped(typeof(Repository<>));

// add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();





app.MapControllers();
app.MapGet("/", () => "test book api");

app.Run();
