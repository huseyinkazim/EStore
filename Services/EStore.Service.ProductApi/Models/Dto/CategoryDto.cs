using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EStore.Service.ProductApi.Models.Dto
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public int? BaseCategoryId { get; set; }
		public Category BaseCategory { get; set; }
	}
}
