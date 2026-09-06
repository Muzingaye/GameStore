namespace GameStore.Api.Dtos;

public record GameDto(
    int Id,
    string name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);