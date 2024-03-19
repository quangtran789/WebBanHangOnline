using System;

namespace WebBanHangOnline.Models
{
    public class ProductClone : ICloneable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public string ProductCode { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal PriceSale { get; set; }
        public string Quantity { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        public int ProductCategoryId { get; set; }
        public string SepTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        // Thêm các thuộc tính khác của sản phẩm nếu cần

        // Phương thức tạo bản sao của sản phẩm
        public object Clone()
        {
            // Tạo một đối tượng ProductClone mới và sao chép giá trị của các thuộc tính
            return new ProductClone
            {
                Id = this.Id,
                Title = this.Title,
                Price = this.Price,
                Description = this.Description,
                Alias = this.Alias,
                ProductCode = this.ProductCode,
                Detail = this.Detail,
                Image = this.Image,
                OriginalPrice = this.OriginalPrice,
                PriceSale = this.PriceSale,
                Quantity = this.Quantity,
                IsHome = this.IsHome,
                IsSale = this.IsSale,
                IsFeature = this.IsFeature,
                IsHot = this.IsHot,
                IsActive = this.IsActive,
                ProductCategoryId = this.ProductCategoryId,
                SepTitle = this.SepTitle,
                SeoDescription = this.SeoDescription,
                SeoKeywords = this.SeoKeywords,
            };
        }
    }
}
