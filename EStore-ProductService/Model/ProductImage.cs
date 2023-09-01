using System.ComponentModel.DataAnnotations;

namespace EStore_ProductService.Model
{

	// Ürün görseli sınıfı
	public class ProductImage
	{
		[Key]
		public int ProductImageId { get; set; }

		[Required]
		public byte[] ImageData { get; set; }

		[Required]
		[StringLength(255)]
		public string ImageMimeType { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}

}
