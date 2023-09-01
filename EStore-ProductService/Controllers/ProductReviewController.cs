using EStore_ProductService.Context;
using EStore_ProductService.Controllers.Base;
using EStore_ProductService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EStore_ProductService.Controllers
{
	

public class ProductReviewController : BaseApiContoller

{
		private readonly ProductDbContext _context;

		public ProductReviewController(ProductDbContext context)
		{
			_context = context;
		}

		// Tüm ürün incelemelerini getir
		public IQueryable<ProductReview> GetAllProductReviews()
		{
			return _context.ProductReviews;
		}

		// Bir ürün incelemesini ID'ye göre getir
		public ProductReview GetProductReviewById(int productReviewId)
		{
			return _context.ProductReviews.Find(productReviewId);
		}

		// Yeni bir ürün incelemesi ekler
		public void AddProductReview(ProductReview productReview)
		{
			_context.ProductReviews.Add(productReview);
			_context.SaveChanges();
		}

		// Bir ürün incelemesini günceller
		public void UpdateProductReview(ProductReview productReview)
		{
			_context.Entry(productReview).State = EntityState.Modified;
			_context.SaveChanges();
		}

		// Bir ürün incelemesini siler
		public void DeleteProductReview(int productReviewId)
		{
			var productReview = _context.ProductReviews.Find(productReviewId);
			if (productReview != null)
			{
				_context.ProductReviews.Remove(productReview);
				_context.SaveChanges();
			}
		}
	}

}
