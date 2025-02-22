using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mission06_Gardner.Models
{
    public class Movie
    {
        [Key]  // Explicitly define MovieID as the primary key
        public int MovieId { get; set; }

        // Nullable CategoryID (assuming CategoryID can be NULL in the database)
        public int? CategoryID { get; set; }

        // Nullable Title (if Title can be NULL)
        public string? Title { get; set; }

        // Nullable Year (if Year can be NULL)
        public int? Year { get; set; }

        // Nullable Director (if Director can be NULL)
        public string? Director { get; set; }

        // Nullable CopiedToPlex (if CopiedToPlex can be NULL)
        
        public int? CopiedToPlex { get; set; }


        // Nullable Rating (if Rating can be NULL)
        public string? Rating { get; set; }

        // Nullable Edited (if Edited can be NULL)
        public int? Edited { get; set; }

        // Nullable LentTo (if LentTo can be NULL)
        public string? LentTo { get; set; }

        // Nullable Notes (if Notes can be NULL)
        public string? Notes { get; set; }
        
        public Category Category { get; set; }
    }
    
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        
        [Required]
        public string CategoryName { get; set; }
        
        public List<Movie> Movies { get; set; } // Navigation property for related movies
    }
}