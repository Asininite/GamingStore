using System.ComponentModel.DataAnnotations;

namespace GameStore.api.Dtos;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name,
//  [Required][StringLength(30)]string Genre,
    int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
