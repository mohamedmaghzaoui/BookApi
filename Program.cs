
using Microsoft.AspNetCore.Builder;
using BookLibraryAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Build the application
var app = builder.Build();

app.MapGet("/", () => "hello");

app.Run();
