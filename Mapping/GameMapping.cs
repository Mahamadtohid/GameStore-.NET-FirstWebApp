namespace GameStore.Api.Mapping;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto createGameDto)
    {

        return new Game()
        {
            Name = createGameDto.Name,
            GenreId = createGameDto.GenreId,
            Price = createGameDto.Price,
            ReleaseDate = createGameDto.ReleaseDate
        };
        
    }

    public static GameDto ToDto(this Game game)
    {
        return new (game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate);
    }
}