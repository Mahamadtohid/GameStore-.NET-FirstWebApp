namespace GameStore.Api.EndPoints;
using Microsoft.EntityFrameworkCore;
using GameStore.Api.Data; 
using GameStore.Api.Dtos;  
using GameStore.Api.Entities;
using GameStore.Api.Mapping;



public static class GameEndPoints// Static class that have extension methods
{
    // var group = AppContext.MapGroup("/api/v1/game").WithTags("Game EndPoints");

    private static readonly List<GameSummaryDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99m, new DateOnly(1992, 7, 15)),
    new (2, "Final Fantasy VII", "Roleplaying", 59.99m, new DateOnly(1997, 1, 31)),
    new (3, "The Legend of Zelda: Ocarina of Time", "Adventure", 69.99m, new DateOnly(1998, 11, 21)),
    new (4, "FIFA 23", "Sports", 69.99m, new DateOnly(2022, 9, 30)),
    new (5, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
    new (6, "Cyberpunk 2077", "Action RPG", 49.99m, new DateOnly(2020, 12, 10)),
    new (7, "Elden Ring", "Souls-like", 59.99m, new DateOnly(2022, 2, 25)),
    new (8, "Stardew Valley", "Simulation", 14.99m, new DateOnly(2016, 2, 26)),
    new (9, "Hades", "Roguelike", 24.99m, new DateOnly(2020, 9, 17)),
    new (10, "Halo: Combat Evolved", "Shooter", 9.99m, new DateOnly(2001, 11, 15))
];

public static RouteGroupBuilder MapGameEndPoints(this WebApplication app)

{
        var group = app.MapGroup("/api/v1/game")
                       .WithTags("Game EndPoints");
        app.MapGet("/", () => "Backend is healthy and Running!");
        group.MapGet("/", (GameStoreContext dbContext) =>
        {
            // var games = dbContext.Games.ToList(); // gets all records
            return dbContext.Games
                .Include(g => g.Genre)
                .Select(games => games.ToGameSummaryDto()).AsNoTracking(); // gets all records and maps to GameSummaryDto
            // return Results.Ok(games);
        });
        group.MapGet("/{id}", (int id , GameStoreContext gameStoreContext) =>
        {
            Game? game = gameStoreContext.Games.Find(id);
            return game is not null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();

        });


        
        group.MapPost("/", (CreateGameDto newGame , GameStoreContext dbContext) =>
        {

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();
 
            return Results.Created($"/api/v1/game/{game.Id}", game.ToGameDetailsDto());
        }).WithParameterValidation();

        group.MapPut("/{id}" , (int id , UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            int index = games.FindIndex(g => g.Id == id);

            var  existingGame = dbContext.Games.Find(id);


            if(existingGame is null) return Results.NotFound();

            dbContext.Entry(existingGame)
                    .CurrentValues
                    .SetValues(updateGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
            
        });

        group.MapDelete("/{id}" , (int id , GameStoreContext dbContext)=>
        {
            dbContext.Games.Where(games => games.Id == id).ExecuteDelete();

            return Results.NoContent();
        });   

    return group;
    }
    
}