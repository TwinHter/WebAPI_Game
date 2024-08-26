using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;
public record class UpdateGameDto(
    [Required][StringLength(50, ErrorMessage = "Name is too long")] string Name,
    [Required] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate    
);
