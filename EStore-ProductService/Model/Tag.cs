using System.ComponentModel.DataAnnotations;

namespace EStore_ProductService.Model
{
	// Etiket sınıfı
	public class Tag
	{
		[Key]
		public int TagId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public ICollection<ProductTag> ProductTags { get; set; }
	}
}
