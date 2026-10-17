using System.ComponentModel.DataAnnotations;

namespace Lab7Layout.Models;

public class Blog
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Title")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1)]
    public int Status { get; set; } = 1;

    [DataType(DataType.Date)]
    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [StringLength(100)]
    [Url]
    [Display(Name = "Image URL")]
    public string? Image { get; set; }

    [StringLength(350)]
    public string? Description { get; set; }
}
