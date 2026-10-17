using Lab7Layout.Data;
using Lab7Layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab7Layout.Areas.Admin.Controllers;

[Area("Admin")]
public class LXHBlogController(Lab7LayoutContext database) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await database.Blogs.OrderByDescending(blog => blog.CreatedDate).ToListAsync());
    }

    public IActionResult Create() => View(new Blog());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Status,Image,Description")] Blog blog)
    {
        if (!ModelState.IsValid)
        {
            return View(blog);
        }

        blog.CreatedDate = DateTime.UtcNow;
        database.Blogs.Add(blog);
        await database.SaveChangesAsync();
        TempData["Success"] = "Blog post created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var blog = await database.Blogs.FindAsync(id);
        return blog is null ? NotFound() : View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Name,Status,Image,Description")] Blog input)
    {
        if (id != input.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(input);
        }

        var blog = await database.Blogs.FindAsync(id);
        if (blog is null)
        {
            return NotFound();
        }

        blog.Name = input.Name;
        blog.Status = input.Status;
        blog.Image = input.Image;
        blog.Description = input.Description;
        await database.SaveChangesAsync();
        TempData["Success"] = "Blog post updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var blog = await database.Blogs.FindAsync(id);
        if (blog is null)
        {
            return NotFound();
        }

        database.Blogs.Remove(blog);
        await database.SaveChangesAsync();
        TempData["Success"] = "Blog post deleted.";
        return RedirectToAction(nameof(Index));
    }
}
