using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskWithCategories.Models;
using TaskWithCategories.Repositories.Contracts;

namespace TaskWithCategories.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryData _categoriesRepository;

        public CategoryController(ICategoryData categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoriesRepository.GetAllCategoriesWithContent();

            return View(categories);
        }

        public IActionResult AddToDB()
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
