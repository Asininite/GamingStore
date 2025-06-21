namespace GameStore.api.Dtos;

public record class GameDetailsDto(
    int id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);



