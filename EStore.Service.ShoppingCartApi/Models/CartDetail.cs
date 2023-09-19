using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using EStore.Service.ShoppingCartApi.Models.Dto;

namespace EStore.Service.ShoppingCartApi.Models
{
	public class CartDetail
	{
		[Key]
		public int CartDetailId { get; set; }
		public int CartHeaderId { get; set; }
		[ForeignKey("CartHeaderId")]
		public CartHeader CartHeader { get; set; }
		public int ProductId { get; set; }
		[NotMapped]
		public ProductDto Product { get; set; }
		public int Count { get; set; }

	}
}
