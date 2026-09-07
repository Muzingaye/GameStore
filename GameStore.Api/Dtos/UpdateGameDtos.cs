namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    string Name,
    string Genre,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);