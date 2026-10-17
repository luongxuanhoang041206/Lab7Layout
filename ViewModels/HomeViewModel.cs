using Lab7Layout.Models;

namespace Lab7Layout.ViewModels;

public class HomeViewModel
{
    public IReadOnlyList<Category> Categories { get; init; } = [];
    public IReadOnlyList<Product> FeaturedProducts { get; init; } = [];
}
