using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
    [Required] string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);