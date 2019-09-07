using System.Collections.Generic;
using TaskWithCategories.Models;

namespace TaskWithCategories.Repositories.Contracts
{
    public interface ICategoryData
    {
        List<Category> GetAllCategoriesWithContent();
        void AddCategory(int? parentCategoryId, string name);
        void AddGoods(Goods goods);
    }
}
