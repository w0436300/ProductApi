namespace ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }            // 唯一标识
        public string? Name { get; set; }      // 商品名称
        public decimal Price { get; set; }     // 价格
        public string? Description { get; set; } // 简要描述
    }
}
