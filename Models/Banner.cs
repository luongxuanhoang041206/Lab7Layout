using System.ComponentModel.DataAnnotations;

namespace Lab7Layout.Models;

public class Banner
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1)]
    public int Status { get; set; } = 1;

    [Range(0, int.MaxValue)]
    public int Priority { get; set; }

    [StringLength(100)]
    [Url]
    [Display(Name = "Image URL")]
    public string? Image { get; set; }

    [StringLength(350)]
    public string? Description { get; set; }
}
