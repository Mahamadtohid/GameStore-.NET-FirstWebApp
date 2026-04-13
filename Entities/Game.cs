namespace GameStore.Api.Entities;
using GameStore.Api.Entities;
public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int GenreId { get; set; }

    public Genre? Genre { get; set; }
    public required decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }   
}