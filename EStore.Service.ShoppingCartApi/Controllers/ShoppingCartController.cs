using AutoMapper;
using EStore.Service.ShoppingCartApi.Context;
using EStore.Service.ShoppingCartApi.Models;
using EStore.Service.ShoppingCartApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EStore.Service.ShoppingCartApi.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class ShoppingCartController : ControllerBase
	{
		private ResponseDto _response;
		private IMapper _mapper;
		private readonly ApplicationDbContext _db;

		public ShoppingCartController(ApplicationDbContext db,
			IMapper mapper)
		{
			_db = db;
			this._response = new ResponseDto();
			_mapper = mapper;
		}

		/// <summary>
		/// Ürün sepete ekle kullanıldığı zaman kullanılacak
		/// </summary>
		/// <param name="cartDto"></param>
		/// <returns></returns>
		[HttpPost("CartInsert")]
		public async Task<ResponseDto> CartInsert(CartDto cartDto)
		{
			try
			{
				var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
					.FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
				if (cartHeaderFromDb == null)
				{
					CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
					_db.CartHeaders.Add(cartHeader);
					await _db.SaveChangesAsync();
					cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
					foreach (var itemDto in cartDto.CartDetails)
						_db.CartDetails.Add(_mapper.Map<CartDetail>(itemDto));
				}
				else
				{
					foreach (var productCartDto in cartDto.CartDetails)
					{
						var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
							u => u.ProductId == productCartDto.ProductId &&
							u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
						if (cartDetailsFromDb == null)
						{
							productCartDto.CartHeaderId = cartHeaderFromDb.CartHeaderId;
							_db.CartDetails.Add(_mapper.Map<CartDetail>(productCartDto));
						}
						else
						{
							productCartDto.Count += cartDetailsFromDb.Count;
							productCartDto.CartHeaderId = cartDetailsFromDb.CartHeaderId;
							productCartDto.CartDetailId = cartDetailsFromDb.CartDetailId;
							_db.CartDetails.Update(_mapper.Map<CartDetail>(productCartDto));
						}
					}
				}
				await _db.SaveChangesAsync();

				_response.Result = cartDto;
			}
			catch (Exception ex)
			{
				_response.Message = ex.Message.ToString();
				_response.IsSuccess = false;
			}
			return _response;
		}

		/// <summary>
		/// Sepetin içerisinde değişiklik yapıldığı zaman kullanılacak
		/// </summary>
		/// <param name="cartDto"></param>
		/// <returns></returns>
		[HttpPut("CartUpdate")]
		public async Task<ResponseDto> CartUpdate(CartDto cartDto)
		{
			try
			{
				var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
					.FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
				if (cartHeaderFromDb == null)
				{
					CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
					_db.CartHeaders.Add(cartHeader);
					await _db.SaveChangesAsync();
					cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
					foreach (var itemDto in cartDto.CartDetails)
						_db.CartDetails.Add(_mapper.Map<CartDetail>(itemDto));
				}
				else
				{
					foreach (var productCartDto in cartDto.CartDetails)
					{
						var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
							u => u.ProductId == productCartDto.ProductId &&
							u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
						if (cartDetailsFromDb == null)
						{
							productCartDto.CartHeaderId = cartHeaderFromDb.CartHeaderId;
							_db.CartDetails.Add(_mapper.Map<CartDetail>(productCartDto));
						}
						else
						{
							productCartDto.Count = cartDetailsFromDb.Count;
							productCartDto.CartHeaderId = cartDetailsFromDb.CartHeaderId;
							productCartDto.CartDetailId = cartDetailsFromDb.CartDetailId;
							_db.CartDetails.Update(_mapper.Map<CartDetail>(productCartDto));
						}
					}
				}
				await _db.SaveChangesAsync();

				_response.Result = cartDto;
			}
			catch (Exception ex)
			{
				_response.Message = ex.Message.ToString();
				_response.IsSuccess = false;
			}
			return _response;
		}


	}
}
