using EStore_ProductService.Context;
using EStore_ProductService.Controllers.Base;
using EStore_ProductService.Model;
using Microsoft.AspNetCore.Mvc;

namespace EStore_ProductService.Controllers
{
	public class CategoryController : BaseApiContoller
	{
		private readonly ProductDbContext _context;

		public CategoryController(ProductDbContext context)
		{
			_context = context;
		}

		// Tüm kategorileri getir
		public IQueryable<Category> GetAllCategories()
		{
			return _context.Categories;
		}

		// Bir kategoriyi ID'ye göre getir
		public Category GetCategoryById(int categoryId)
		{
			return _context.Categories.Find(categoryId);
		}

		// Yeni bir kategori ekler
		public void AddCategory(Category category)
		{
			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		// Bir kategoriyi günceller
		public void UpdateCategory(Category category)
		{
			_context.Categories.Update(category);
			_context.SaveChanges();
		}

		// Bir kategoriyi siler
		public void DeleteCategory(int categoryId)
		{
			var category = _context.Categories.Find(categoryId);
			if (category != null)
			{
				_context.Categories.Remove(category);
				_context.SaveChanges();
			}
		}
	}
}
