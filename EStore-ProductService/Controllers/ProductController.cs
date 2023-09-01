using EStore_ProductService.Context;
using EStore_ProductService.Controllers.Base;
using EStore_ProductService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EStore_ProductService.Controllers
{
	public class ProductController : BaseApiContoller
	{
		private readonly ProductDbContext _context;

		public ProductController(ProductDbContext context)
		{
			_context = context;
		}
		#region query
		[HttpGet]
		public List<Product> GetAllProducts()
		{
			return _context.Products.Include(p => p.Category)
									.Include(p => p.ProductTags)
										.ThenInclude(pt => pt.Tag)
									.ToList();
		}
		[HttpGet]
		public Product? GetProductById(int productId)
		{
			return _context.Products.Include(p => p.Category)
									.Include(p => p.ProductTags)
										.ThenInclude(pt => pt.Tag)
								.FirstOrDefault(p => p.ProductId == productId);
		}

		[HttpGet]
		public List<Product> GetProductsByCategory(int categoryId)
		{
			return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
		}

		#endregion
		#region command
		/// <summary>
		/// 
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult AddProduct([FromBody] Product product)
		{
			_context.Products.Add(product);
			_context.SaveChanges();

			return Ok();
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="product"></param>
		/// <returns></returns>
		[HttpPut]
		public IActionResult UpdateProduct(int id, [FromBody] Product product)
		{
			if (_context.Products.Any(r => r.ProductId == id))
			{
				_context.Products.Update(product);
				_context.SaveChanges();
			}
			return Ok();

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		public IActionResult DeleteProduct(int id)
		{
			if (_context.Products.Any(r => r.ProductId == id))
			{
				Product rec = new Product() { ProductId = id };
				_context.Entry(rec).State = EntityState.Deleted;
				_context.SaveChanges();
			}

			return Ok();

		}
	
		// Bir ürüne etiket ekler
		public void AddTagToProduct(int productId, int tagId)
		{
			var product = _context.Products.Include(p => p.ProductTags).FirstOrDefault(p => p.ProductId == productId);
			var tag = _context.Tags.Find(tagId);

			if (product != null && tag != null)
			{
				if (product.ProductTags == null)
					product.ProductTags = new List<ProductTag>();

				product.ProductTags.Add(new ProductTag { Product = product, Tag = tag });
				_context.SaveChanges();
			}
		}

		// Bir üründen etiketi kaldırır
		public void RemoveTagFromProduct(int productId, int tagId)
		{
			var product = _context.Products.Include(p => p.ProductTags).FirstOrDefault(p => p.ProductId == productId);
			var tag = _context.Tags.Find(tagId);

			if (product != null && tag != null && product.ProductTags != null)
			{
				var productTag = product.ProductTags.FirstOrDefault(pt => pt.TagId == tagId);
				if (productTag != null)
				{
					product.ProductTags.Remove(productTag);
					_context.SaveChanges();
				}
			}
		}
		#endregion

	}
}
