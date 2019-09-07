using System.Collections.Generic;
using TaskWithCategories.Models;

namespace TaskWithCategories.Views.Categories
{
    public class TreeViewModel
    {
        public int? CategoryId { get; set; }

        public List<Category> Categories { get;set; }

        public Goods Goods { get; set; }
    }
}
