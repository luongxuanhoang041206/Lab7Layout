using System.ComponentModel.DataAnnotations;

namespace Lab7Layout.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Product name")]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "999999999")]
    public decimal Price { get; set; }

    [Range(typeof(decimal), "0", "999999999")]
    [Display(Name = "Sale price")]
    public decimal SalePrice { get; set; }

    [Range(0, 1)]
    public int Status { get; set; } = 1;

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Created date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [StringLength(100)]
    [Url]
    [Display(Name = "Image URL")]
    public string? Image { get; set; }

    [StringLength(350)]
    public string? Description { get; set; }

    public Category? Category { get; set; }
}
