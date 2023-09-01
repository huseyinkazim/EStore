using System.ComponentModel.DataAnnotations;

namespace EStore_ProductService.Model
{
	// Ürün incelemesi sınıfı
	public class ProductReview
	{
		[Key]
		public int ProductReviewId { get; set; }

		[Required]
		[StringLength(500)]
		public string ReviewText { get; set; }

		[Required]
		[Range(1, 5)]
		public int Rating { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }

	}

}
