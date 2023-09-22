using System.ComponentModel.DataAnnotations;

namespace EStore.Service.ProductApi.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string CategoryName { get; set; }
	}
}
