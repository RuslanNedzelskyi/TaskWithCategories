using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskWithCategories.Models;
using TaskWithCategories.Repositories.Contracts;
using TaskWithCategories.Views.Categories;

namespace TaskWithCategories.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryData _categoriesRepository;

        public CategoriesController(ICategoryData categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            TreeViewModel treeViewModel = new TreeViewModel
            {
                Categories = _categoriesRepository.GetAllCategoriesWithContent(),
                CategoryId = categoryId
            };

            return View(treeViewModel);
        }

        [HttpPost]
        public IActionResult AddCategory(int? categoryId, string categoryName)
        {
            _categoriesRepository.AddCategory(categoryId, categoryName);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddGoods(int? categoryId)
        {
            _categoriesRepository.AddGoods(categoryId);

            return RedirectToAction("Index");
        }

        public IActionResult CategoriesWithContent()
        {
            List<Category> categories = _categoriesRepository.GetAllCategoriesWithContent();

            return View(categories);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
