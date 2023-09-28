using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EStore.Service.ProductApi.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public int? BaseCategoryId { get; set; }
		[JsonIgnore]
		[ForeignKey("BaseCategoryId")]
		public Category BaseCategory { get; set; }
		[JsonIgnore]
		public List<Category> SubCategories { get; set; }
	}
}
