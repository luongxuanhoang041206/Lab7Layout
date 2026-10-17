using Lab7Layout.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab7Layout.Areas.Admin.Controllers;

[Area("Admin")]
public class LXHDashboardController(Lab7LayoutContext database) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["CategoryCount"] = await database.Categories.CountAsync();
        ViewData["ProductCount"] = await database.Products.CountAsync();
        ViewData["BannerCount"] = await database.Banners.CountAsync();
        ViewData["BlogCount"] = await database.Blogs.CountAsync();
        return View();
    }
}
