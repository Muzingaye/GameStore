namespace GameStore.Api.Dtos;

public record GameDetailDto(
    int Id,
    string name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);