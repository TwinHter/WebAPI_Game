using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;
public record class GameDetailsDto(
    int ID,
    [Required][StringLength(50, ErrorMessage = "Name is too long")] string Name,
    [Required] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);