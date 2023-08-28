using System.ComponentModel.DataAnnotations;

namespace Top10Films.Models
{
    public class FilmsModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The string length must be between 3 and 50 characters")]
        [Display(Name = "Movie title")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The string length must be between 3 and 50 characters")]
        [Display(Name = "Director")]
        public string Director { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The string length must be between 3 and 50 characters")]
        [Display(Name = "Genre")]
        public string Genre { get; set; }
        public string? PosterURL { get; set; }

        [Required]
        [Display(Name = "Year")]
        [Range(1900, 2023)]
        public int Year { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The string length must be between 3 and 50 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}