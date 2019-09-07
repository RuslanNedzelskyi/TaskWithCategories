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

        [HttpGet]
        public IActionResult NewGoods(int? subCategoryId)
        {
            return View(subCategoryId);
        }

        [HttpPost]
        public IActionResult AddGoods(Goods goods)
        {
            _categoriesRepository.AddGoods(goods);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddCategory(int? categoryId, string categoryName)
        {
            _categoriesRepository.AddCategory(categoryId, categoryName);

            return RedirectToAction("Index");
        }

        public IActionResult DisplayTree()
        {
            List<Category> categories = _categoriesRepository.GetAllCategoriesWithContent();

            return View(categories);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult DisplayTree(int? categoryId)
        {
            TreeViewModel treeViewModel = new TreeViewModel
            {
                Categories = _categoriesRepository.GetAllCategoriesWithContent(),
                CategoryId = categoryId
            };

            return View(treeViewModel);
        }
    }
}
