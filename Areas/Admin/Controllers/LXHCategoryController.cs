using Lab7Layout.Data;
using Lab7Layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab7Layout.Areas.Admin.Controllers;

[Area("Admin")]
public class LXHCategoryController(Lab7LayoutContext database) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await database.Categories
            .OrderBy(category => category.Name)
            .ToListAsync());
    }

    public IActionResult Create() => View(new Category());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Status")] Category category)
    {
        if (await database.Categories.AnyAsync(existing => existing.Name == category.Name))
        {
            ModelState.AddModelError(nameof(category.Name), "A category with this name already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        category.CreatedDate = DateTime.UtcNow;
        database.Categories.Add(category);
        try
        {
            await database.SaveChangesAsync();
            TempData["Success"] = "Category created.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(category.Name), "The category could not be saved. Check that its name is unique.");
            return View(category);
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var category = await database.Categories.FindAsync(id);
        return category is null ? NotFound() : View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] Category input)
    {
        if (id != input.Id)
        {
            return NotFound();
        }

        if (await database.Categories.AnyAsync(existing =>
                existing.Id != id && existing.Name == input.Name))
        {
            ModelState.AddModelError(nameof(input.Name), "A category with this name already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(input);
        }

        var category = await database.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        category.Name = input.Name;
        category.Status = input.Status;
        try
        {
            await database.SaveChangesAsync();
            TempData["Success"] = "Category updated.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(input.Name), "The category could not be saved. Check that its name is unique.");
            return View(input);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await database.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        if (await database.Products.AnyAsync(product => product.CategoryId == id))
        {
            TempData["Error"] = "This category still has products and cannot be deleted.";
            return RedirectToAction(nameof(Index));
        }

        database.Categories.Remove(category);
        await database.SaveChangesAsync();
        TempData["Success"] = "Category deleted.";
        return RedirectToAction(nameof(Index));
    }
}
