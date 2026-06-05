using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // 🔹 GET: Category/Index
    public IActionResult Index(string search, int? page)
    {
        var categories = _unitOfWork.Categories.GetAll();

        if (!string.IsNullOrEmpty(search))
        {
            categories = categories
                .Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        return View(categories.ToPagedList(page ?? 1, 5));
    }

    // 🔹 GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // 🔹 POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Categories.Add(category);
            _unitOfWork.Save();

            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        return View(category);
    }

    // 🔹 GET: Category/Edit
    public IActionResult Edit(int id)
    {
        var category = _unitOfWork.Categories.GetById(id);

        if (category == null)
            return NotFound();

        return View(category);
    }

    // 🔹 POST: Category/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Categories.Update(category);
            _unitOfWork.Save();

            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        return View(category);
    }

    // 🔹 GET: Category/Delete
    public IActionResult Delete(int id)
    {
        var category = _unitOfWork.Categories.GetById(id);

        if (category == null)
            return NotFound();

        return View(category);
    }

    // 🔹 POST: Category/DeleteConfirmed
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _unitOfWork.Categories.GetById(id);

        if (category == null)
            return NotFound();

        _unitOfWork.Categories.Delete(category);
        _unitOfWork.Save();

        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}