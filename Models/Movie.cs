// We need this namespace to use validation attributes like [Required] and [Range].
using System.ComponentModel.DataAnnotations;

// Namespaces group related code. "Mission_6.Models" means this class lives in the Models folder of our project.
namespace Mission_6.Models;

/// <summary>
/// Represents a single movie in Joel's collection.
/// This is a "model" class: it describes the shape of our data and the rules for validating it.
/// Each property will become a column in the SQLite database (except we use attributes to add rules).
/// </summary>
public class Movie
{
    // Primary key: a unique number for each movie. The database generates this automatically when we add a new movie.
    public int Id { get; set; }

    // The ? means "nullable" — this property can hold text or be empty/null. We use nullable so we can show an empty form when the user first loads the "Add Movie" page.
    // [Required] means the user must enter something before the form can be submitted. We show ErrorMessage if they leave it blank.
    [Required(ErrorMessage = "Category is required.")]
    public string? Category { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    public string? Title { get; set; }

    // Year is an int (whole number), not nullable, so it always has a value. Default is 0 until the user enters a year.
    [Required(ErrorMessage = "Year is required.")]
    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Director is required.")]
    public string? Director { get; set; }

    // [RegularExpression] checks that the value matches a pattern. Here we only allow G, PG, PG-13, or R.
    [Required(ErrorMessage = "Rating is required.")]
    [RegularExpression(@"^(G|PG|PG-13|R)$", ErrorMessage = "Rating must be G, PG, PG-13, or R.")]
    public string? Rating { get; set; }

    // bool = true or false. "Edited" means whether Joel has an edited (e.g. family-friendly) version. Default is false.
    public bool Edited { get; set; }

    // [Display(Name = "Lent To")] controls the label shown on the form. Without it, the label would say "LentTo".
    // Optional field: no [Required], so the user can leave it blank.
    [Display(Name = "Lent To")]
    public string? LentTo { get; set; }

    // [StringLength(25)] means Notes can be at most 25 characters. Optional (no [Required]).
    [StringLength(25, ErrorMessage = "Notes cannot exceed 25 characters.")]
    public string? Notes { get; set; }
}
