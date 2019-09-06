namespace TaskWithCategories.Models
{
    public class Goods
    {
        public double ID { get; set; }

        public string GoodsName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int SubCategoryId { get; set; }

        public virtual Category SubCategory { get; set; }
    }
}
