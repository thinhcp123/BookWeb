using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryRepository _context;

    public CategoryController(ICategoryRepository context)
    {
        _context = context;
    }

    //Get category/page
    public IActionResult Index()
    {
        var category = _context.GetAll();
        return View(category);
    }

    //Get category/create
    public IActionResult Create() => View();

    //POST category/create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
        }
        if (ModelState.IsValid)
        {
            _context.Add(category);
            _context.Save();
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
        var categoryFromDbFirst = _context.GetFirstOrDefault(x => x.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }

        return View(categoryFromDbFirst);
    }

    //POST category/edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
        }
        if (ModelState.IsValid)
        {
            _context.Update(category);
            _context.Save();

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

        var categoryDelete = _context.GetFirstOrDefault(x => x.Id == id);

        if (categoryDelete == null)
        {
            return NotFound();
        }
        return View(categoryDelete);
    }

    //POST category/delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var categoryDelete = _context.GetFirstOrDefault(x => x.Id == id);
        if (categoryDelete == null)
        {
            return NotFound();
        }
        _context.Remove(categoryDelete);
        _context.Save();

        TempData["Success"] = "Category Delete successfully";
        return RedirectToAction("Index");
    }
}
