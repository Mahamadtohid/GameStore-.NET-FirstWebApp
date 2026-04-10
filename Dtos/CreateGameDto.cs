namespace GameStore.Api.Dtos.CreateGameDto.cs;

public record class CreateGameDto(
        // int Id,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
){}