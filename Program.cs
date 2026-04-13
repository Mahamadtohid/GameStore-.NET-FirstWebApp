using GameStore.Api.EndPoints;
using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("GameStore")));
    
var app = builder.Build();



app.MapGet("/", () => "Hello World!"); 

app.MapGameEndPoints(); // Extension method to map all game endpoints

app.Run();
