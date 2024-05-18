using Core.Entities;

namespace Skinet.API.DTOs
{
    public class ProdcutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
  
        public string ProdcutBrands { get; set; }
      
    }
}
