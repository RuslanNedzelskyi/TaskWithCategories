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
                                @"ORDER BY (CASE WHEN c.ParentCategoryId IS NULL THEN 0 ELSE 1 END)";

            List<Category> categories = new List<Category>();

            Category category;

            Category subCategory;

            Goods goods;

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

                        category = categories.FirstOrDefault(c => c.ID.Equals(categoryId));

                        if (string.IsNullOrEmpty(sqlDataReader[7].ToString())
                            || sqlDataReader[7].ToString() == "null")
                        {
                            category = new Category
                            {
                                ID = categoryId,
                                CategoryName = sqlDataReader[6].ToString()
                            };

                            categories.Add(category);
                        }
                        else
                        {
                            int parentCategoryId = int.Parse(sqlDataReader[7].ToString());

                            foreach (Category cat in categories)
                            {
                                if (cat.ID.Equals(parentCategoryId))
                                {
                                    if (!cat.SubCategories.Any(c => c.ID == categoryId))
                                    {
                                        subCategory = new Category
                                        {
                                            ID = categoryId,
                                            CategoryName = sqlDataReader[6].ToString(),
                                            ParentCategoryId = cat.ID
                                        };

                                        if (!string.IsNullOrEmpty(sqlDataReader[0].ToString())
                                            || sqlDataReader[0].ToString() != "null")
                                        {
                                            goods = new Goods
                                            {
                                                ID = int.Parse(sqlDataReader[0].ToString()),
                                                GoodsName = sqlDataReader[1].ToString(),
                                                Description = sqlDataReader[2].ToString(),
                                                Price = double.Parse(sqlDataReader[3].ToString()),
                                                SubCategoryId = int.Parse(sqlDataReader[4].ToString())
                                            };

                                            subCategory.Goods.Add(goods);
                                        }

                                        cat.SubCategories.Add(subCategory);
                                    }
                                    else
                                    {
                                        subCategory = cat.SubCategories.FirstOrDefault(g => g.ID == categoryId);

                                        if (!string.IsNullOrEmpty(sqlDataReader[0].ToString())
                                                || sqlDataReader[0].ToString() != "null")
                                        {
                                            goods = new Goods
                                            {
                                                ID = int.Parse(sqlDataReader[0].ToString()),
                                                GoodsName = sqlDataReader[1].ToString(),
                                                Description = sqlDataReader[2].ToString(),
                                                Price = double.Parse(sqlDataReader[3].ToString()),
                                                SubCategoryId = int.Parse(sqlDataReader[4].ToString())
                                            };

                                            subCategory.Goods.Add(goods);
                                        }
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

        public Category GetCategoryById(int? categoryId)
        {
            string sqlQuery = @"SELECT * FROM Categories " +
                                 $"WHERE ID = {categoryId}";

            Category category = new Category();

            using (SqlConnection connection
                        = new SqlConnection(PathToDB.PATH_TO_DB))
            {
                SqlCommand getCategyById =
                    new SqlCommand(sqlQuery, connection);

                try
                {
                    connection.Open();

                    SqlDataReader sqlDataReader = getCategyById.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        category.CategoryName = sqlDataReader[1].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return category;
        }
    }
}
