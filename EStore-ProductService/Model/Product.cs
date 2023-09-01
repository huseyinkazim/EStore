using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EStore_ProductService.Model
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Price { get; set; }

		[Required]
		[MaxLength(500)]
		public string Description { get; set; }

		[Required]
		public int StockQuantity { get; set; }

		//todo: Diğer özellikler (Marka, Boyutlar, Renkler, vb.) burada eklenebilir.
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public ICollection<ProductTag> ProductTags { get; set; }

		public ICollection<ProductImage> ProductImages { get; set; }

		public ICollection<ProductReview> ProductReviews { get; set; }
	}

}
