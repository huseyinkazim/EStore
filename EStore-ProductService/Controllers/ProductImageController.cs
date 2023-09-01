using EStore_ProductService.Context;
using EStore_ProductService.Controllers.Base;
using EStore_ProductService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EStore_ProductService.Controllers
{
	public class ProductImageController : BaseApiContoller
	{
		private readonly ProductDbContext _context;

		public ProductImageController(ProductDbContext context)
		{
			_context = context;
		}

		// Tüm ürün görsellerini getir
		public IQueryable<ProductImage> GetAllProductImages()
		{
			return _context.ProductImages;
		}

		// Bir ürün görselini ID'ye göre getir
		public ProductImage GetProductImageById(int productImageId)
		{
			return _context.ProductImages.Find(productImageId);
		}

		// Yeni bir ürün görseli ekler
		public void AddProductImage(ProductImage productImage)
		{
			_context.ProductImages.Add(productImage);
			_context.SaveChanges();
		}

		// Bir ürün görselini günceller
		public void UpdateProductImage(ProductImage productImage)
		{
			_context.Entry(productImage).State = EntityState.Modified;
			_context.SaveChanges();
		}

		// Bir ürün görselini siler
		public void DeleteProductImage(int productImageId)
		{
			var productImage = _context.ProductImages.Find(productImageId);
			if (productImage != null)
			{
				_context.ProductImages.Remove(productImage);
				_context.SaveChanges();
			}
		}
	}

}
