using System.Collections.Generic;

namespace TaskWithCategories.Models
{
    public class Category
    {
        public Category()
        {
            SubCategories = new HashSet<Category>();

            Goods = new HashSet<Goods>();
        }

        public int ID { get; set; }

        public string CategoryName { get; set; }

        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Goods> Goods { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
