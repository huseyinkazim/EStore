using System.ComponentModel.DataAnnotations;

namespace EStore.Service.ProductApi.Models.Dto
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
	}
}
