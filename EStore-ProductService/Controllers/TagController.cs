using EStore_ProductService.Context;
using EStore_ProductService.Controllers.Base;
using EStore_ProductService.Model;

namespace EStore_ProductService.Controllers
{

	public class TagController : BaseApiContoller
	{
		private readonly ProductDbContext _context;

		public TagController(ProductDbContext context)
		{
			_context = context;
		}

		// Tüm etiketleri getir
		public IQueryable<Tag> GetAllTags()
		{
			return _context.Tags;
		}

		// Bir etiketi ID'ye göre getir
		public Tag GetTagById(int tagId)
		{
			return _context.Tags.Find(tagId);
		}

		// Yeni bir etiket ekler
		public void AddTag(Tag tag)
		{
			_context.Tags.Add(tag);
			_context.SaveChanges();
		}

		// Bir etiketi günceller
		public void UpdateTag(Tag tag)
		{
			_context.Tags.Update(tag);
			_context.SaveChanges();
		}

		// Bir etiketi siler
		public void DeleteTag(int tagId)
		{
			var tag = _context.Tags.Find(tagId);
			if (tag != null)
			{
				_context.Tags.Remove(tag);
				_context.SaveChanges();
			}
		}
	}

}
