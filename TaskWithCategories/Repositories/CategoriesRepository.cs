using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TaskWithCategories.Models;
using TaskWithCategories.Repositories.Consts;
using TaskWithCategories.Repositories.Contracts;

namespace TaskWithCategories.Repositories
{
    public class CategoriesRepository : ICategoryData
    {
        public List<Category> GetAllCategoriesWithContent()
        {
            string sqlQuery = @"SELECT g.ID, g.GoodsName, g.Description, g.Price, g. SubCategoryId, " +
                                    @"c.ID, c.CategoryName, c.ParentCategoryId " +
                                    @"FROM Goods AS g RIGHT JOIN Categories AS c " +
                                    @"ON g.SubCategoryId = c.ID " +
                                    @"ORDER BY c.ParentCategoryId";

            List<Category> categories = new List<Category>();
            List<Category> SubCategories = new List<Category>();
            using (SqlConnection connection
                = new SqlConnection(PathToDB.PATH_TO_DB))
            {
                SqlCommand getCategoriesWithContent =
                    new SqlCommand(sqlQuery, connection);

                try
                {
                    connection.Open();

                    SqlDataReader sqlDataReader = getCategoriesWithContent.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        int categoryId = int.Parse(sqlDataReader[5].ToString());

                        Category category = categories.FirstOrDefault(c => c.ID.Equals(categoryId));

                        if (category == null)
                        {
                            foreach (Category cat in categories)
                            {
                                if (!cat.SubCategories.Any(j => j.ID == categoryId))
                                {
                                    category = new Category
                                    {
                                        ID = categoryId,
                                        CategoryName = sqlDataReader[6].ToString(),

                                    };
                                }
                                else
                                {
                                    category = cat.SubCategories.FirstOrDefault(j => j.ID == categoryId);
                                }
                            }

                            if (string.IsNullOrEmpty(sqlDataReader[7].ToString()) || sqlDataReader[7].ToString().ToLower() != "null")
                            {
                                category = new Category
                                {
                                    ID = categoryId,
                                    CategoryName = sqlDataReader[6].ToString(),

                                };
                                category.ParentCategoryId = null;
                                categories.Add(category);
                            }
                            else
                            {
                                category.ParentCategoryId = int.Parse(sqlDataReader[7].ToString());
                                Category parentCategory = categories.FirstOrDefault(c => c.ID == category.ParentCategoryId);
                                if (parentCategory.ID == category.ParentCategoryId)
                                {
                                    if (!parentCategory.SubCategories.Any(c => c.ID == category.ID))
                                    {
                                        parentCategory.SubCategories.Add(category);
                                        SubCategories.Add(category);
                                    }
                                }
                            }

                            foreach (Category cat in categories)
                            {
                                if (cat.SubCategories.Any(g => g.ID == int.Parse(sqlDataReader[5].ToString())))
                                {
                                    if (!string.IsNullOrEmpty(sqlDataReader[0].ToString()))
                                    {
                                        Goods goods = new Goods
                                        {
                                            ID = int.Parse(sqlDataReader[0].ToString()),
                                            Description = sqlDataReader[2].ToString(),
                                            GoodsName = sqlDataReader[1].ToString(),
                                            Price = double.Parse(sqlDataReader[3].ToString()),
                                            SubCategoryId = categoryId
                                        };
                                        category.Goods.Add(goods);
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return categories;
        }
    }
}
