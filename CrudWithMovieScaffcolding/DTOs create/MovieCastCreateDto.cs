using System.ComponentModel.DataAnnotations;

namespace CrudWithMovieScaffcolding.DTOs_create
{
    public class MovieCastCreateDto
    {
        [Required]
        public int MovId { get; set; }

        [Required]
        public int ActId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Role cannot exceed 30 characters.")]
        public string? Role { get; set; }
    }
}
