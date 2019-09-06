using System.Collections.Generic;
using TaskWithCategories.Models;
using TaskWithCategories.Repositories.Contracts;

namespace TaskWithCategories.Repositories
{
    public class CategoriesRepository : ICategoryData
    {
        public List<Category> GetAllCategoriesWithContent()
        {
            string sqlQuery = "";
            List<Category> categories = new List<Category>();

            return categories;
        }
    }
}
