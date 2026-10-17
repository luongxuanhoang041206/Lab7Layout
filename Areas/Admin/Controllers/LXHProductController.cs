using Lab7Layout.Data;
using Lab7Layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab7Layout.Areas.Admin.Controllers;

[Area("Admin")]
public class LXHProductController(Lab7LayoutContext database) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await database.Products
            .Include(product => product.Category)
            .OrderBy(product => product.Name)
            .ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        await LoadCategoriesAsync();
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Name,Price,SalePrice,Status,CategoryId,Image,Description")] Product product)
    {
        if (!await database.Categories.AnyAsync(category => category.Id == product.CategoryId))
        {
            ModelState.AddModelError(nameof(product.CategoryId), "Choose a valid category.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return View(product);
        }

        product.CreatedDate = DateTime.UtcNow;
        database.Products.Add(product);
        await database.SaveChangesAsync();
        TempData["Success"] = "Product created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await database.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        await LoadCategoriesAsync();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Name,Price,SalePrice,Status,CategoryId,Image,Description")] Product input)
    {
        if (id != input.Id)
        {
            return NotFound();
        }

        if (!await database.Categories.AnyAsync(category => category.Id == input.CategoryId))
        {
            ModelState.AddModelError(nameof(input.CategoryId), "Choose a valid category.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return View(input);
        }

        var product = await database.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        product.Name = input.Name;
        product.Price = input.Price;
        product.SalePrice = input.SalePrice;
        product.Status = input.Status;
        product.CategoryId = input.CategoryId;
        product.Image = input.Image;
        product.Description = input.Description;
        await database.SaveChangesAsync();
        TempData["Success"] = "Product updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await database.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        database.Products.Remove(product);
        await database.SaveChangesAsync();
        TempData["Success"] = "Product deleted.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategoriesAsync()
    {
        ViewBag.Categories = new SelectList(
            await database.Categories.OrderBy(category => category.Name).ToListAsync(),
            nameof(Category.Id),
            nameof(Category.Name));
    }
}
