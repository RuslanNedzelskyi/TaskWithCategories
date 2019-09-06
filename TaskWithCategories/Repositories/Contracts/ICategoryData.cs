using System.Collections.Generic;
using TaskWithCategories.Models;

namespace TaskWithCategories.Repositories.Contracts
{
    public interface ICategoryData
    {
        List<Category> GetAllCategoriesWithContent();
    }
}
