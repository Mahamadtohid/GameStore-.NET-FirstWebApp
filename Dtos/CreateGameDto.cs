        using System.ComponentModel.DataAnnotations;
        namespace GameStore.Api.Dtos;

        public record class CreateGameDto(
                // int Id,
                [Required]string Name,
                // [Required]string Genre,
                [Required] int GenreId,
                [Required][Range(0, 1000)] decimal Price,
                DateOnly ReleaseDate
        ){}