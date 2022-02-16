using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers;
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    //Get category/page
    public IActionResult Index()
    {
        var category = _context.Categories.ToList();
        return View(category);
    }

    //Get category/create
    public IActionResult Create() => View();

    //POST category/create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
        }
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(category);

    }
    //Get category/Edit
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDb = _context.Categories.Find(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    //POST category/edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
        }
        if (ModelState.IsValid)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Category Update successfully";

            return RedirectToAction("Index");
        }

        return View(category);
    }

    //Get category/delete
    public IActionResult Delete(int? id)
    {
        if (id == null && id == 0)
        {
            return NotFound();
        }

        var categoryDelete = _context.Categories.Find(id);

        if (categoryDelete == null)
        {
            return NotFound();
        }
        return View(categoryDelete);
    }

    //POST category/delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePOST(int? id)
    {
        var categoryDelete = await _context.Categories.FindAsync(id);
        if (categoryDelete == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(categoryDelete);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Category Delete successfully";
        return RedirectToAction("Index");
    }
}
