using Lab7Layout.Data;
using Lab7Layout.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab7Layout.Controllers;

public class LXHHomeController(Lab7LayoutContext database) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Categories = await database.Categories
                .Where(category => category.Status == 1)
                .OrderBy(category => category.Name)
                .ToListAsync(),
            FeaturedProducts = await database.Products
                .Include(product => product.Category)
                .Where(product => product.Status == 1)
                .OrderByDescending(product => product.Id)
                .Take(6)
                .ToListAsync()
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
