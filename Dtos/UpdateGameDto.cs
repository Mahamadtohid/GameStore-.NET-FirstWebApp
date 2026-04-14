namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
        // int Id,
        string Name,
        int GenreId,
        decimal Price
        // DateOnly ReleaseDate
){}