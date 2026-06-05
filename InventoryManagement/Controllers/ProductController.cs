using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _env;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public IActionResult Index(string search, int? page)
    {
        var products = _unitOfWork.Products.GetAll();

        if (!string.IsNullOrEmpty(search))
        {
            products = products.Where(p => p.Name.Contains(search));
        }

        return View(products.ToPagedList(page ?? 1, 5));
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _unitOfWork.Categories.GetAll();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_env.WebRootPath, "images", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);

                product.ImagePath = fileName;
            }

            _unitOfWork.Products.Add(product);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }

        ViewBag.Categories = _unitOfWork.Categories.GetAll();
        return View(product);
    }

    public IActionResult Edit(int id)
    {
        var product = _unitOfWork.Products.GetById(id);
        ViewBag.Categories = _unitOfWork.Categories.GetAll();
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            var File = product.clientfile;
            if (file != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_env.WebRootPath, "images", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);

                product.ImagePath = fileName;
            }

            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToAction("Index");
        }
        ViewBag.Categories = _unitOfWork.Categories.GetAll();
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        var product = _unitOfWork.Products.GetById(id);
        return View(product);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _unitOfWork.Products.GetById(id);

        _unitOfWork.Products.Delete(product);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
    }
}