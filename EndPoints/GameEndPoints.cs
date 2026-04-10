namespace GameStore.Api.EndPoints.GameEndPoints.cs;
using GameStore.Api.Dtos.GameDto.cs;
using GameStore.Api.Dtos.CreateGameDto.cs;
using GameStore.Api.Dtos.UpdateGameDto.cs;



public static class GameEndPoints// Static class that have extension methods
{
    var group = AppContext.MapGroup("/api/v1/game").WithTags("Game EndPoints");

    private static readonly List<GameDto> games = [
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
        app.MapGet("/", () => "Backend is healthy and Running!");
        group.MapGet("/", () => games);
        group.MapGet("/{id}", (int id) => games.Find(g => g.Id == id) is GameDto game ? Results.Ok(game) : Results.NotFound());
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new (games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
            games.Add(game);
            return Results.Created($"/api/v1/game/{game.Id}", game);
        });

        group.MapPut("/{id}" , (int id , UpdateGameDto updateGame) =>
        {
            int index = games.FindIndex(g => g.Id == id);


            if(index == -1) return Results.NotFound();

            games[index] = new GameDto(id, updateGame.Name, updateGame.Genre, updateGame.Price, games[index].ReleaseDate);

            return Results.NoContent();
            
        });

        group.MapDelete("/{id}" , (int id)=>
        {
            int index = games.FindIndex(g => g.Id == id);
            if(index == -1) return Results.NotFound();

            games.RemoveAt(index);
            return Results.NoContent();
        });   

    return group;
    }
    
}