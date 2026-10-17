using Lab7Layout.Data;
using Lab7Layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab7Layout.Areas.Admin.Controllers;

[Area("Admin")]
public class LXHBannerController(Lab7LayoutContext database) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await database.Banners.OrderBy(banner => banner.Priority).ToListAsync());
    }

    public IActionResult Create() => View(new Banner());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Status,Priority,Image,Description")] Banner banner)
    {
        if (!ModelState.IsValid)
        {
            return View(banner);
        }

        database.Banners.Add(banner);
        await database.SaveChangesAsync();
        TempData["Success"] = "Banner created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var banner = await database.Banners.FindAsync(id);
        return banner is null ? NotFound() : View(banner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Name,Status,Priority,Image,Description")] Banner input)
    {
        if (id != input.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(input);
        }

        var banner = await database.Banners.FindAsync(id);
        if (banner is null)
        {
            return NotFound();
        }

        banner.Name = input.Name;
        banner.Status = input.Status;
        banner.Priority = input.Priority;
        banner.Image = input.Image;
        banner.Description = input.Description;
        await database.SaveChangesAsync();
        TempData["Success"] = "Banner updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var banner = await database.Banners.FindAsync(id);
        if (banner is null)
        {
            return NotFound();
        }

        database.Banners.Remove(banner);
        await database.SaveChangesAsync();
        TempData["Success"] = "Banner deleted.";
        return RedirectToAction(nameof(Index));
    }
}
