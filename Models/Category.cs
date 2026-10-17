using System.ComponentModel.DataAnnotations;

namespace Lab7Layout.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Category name")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1)]
    [Display(Name = "Status")]
    public int Status { get; set; } = 1;

    [DataType(DataType.Date)]
    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
