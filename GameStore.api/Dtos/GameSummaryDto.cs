using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;
public record class GameSummaryDto(
    int ID,
    [Required][StringLength(50, ErrorMessage = "Name is too long")] string Name,
    [Required][StringLength(10)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);