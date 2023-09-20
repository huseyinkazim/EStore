using AutoMapper;
using EStore.Service.ShoppingCartApi.Context;
using EStore.Service.ShoppingCartApi.Models;
using EStore.Service.ShoppingCartApi.Models.Dto;
using EStore.Service.ShoppingCartApi.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EStore.Service.ShoppingCartApi.Controllers
{
	[ApiController]
	//[Authorize]
	[Route("api/[controller]")]
	public class ShoppingCartController : ControllerBase
	{
		private ResponseDto _response;
		private IMapper _mapper;
		private readonly ApplicationDbContext _db;
		private readonly IProductService _productService;
		private readonly ICouponService _couponService;

		public ShoppingCartController(ApplicationDbContext db,
			IMapper mapper, ICouponService couponService, IProductService productService)
		{
			_db = db;
			this._response = new ResponseDto();
			_mapper = mapper;
			_couponService = couponService;
			_productService = productService;
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

		[HttpDelete("RemoveCart")]
		public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
		{
			try
			{
				CartDetail cartDetail = _db.CartDetails
				   .First(u => u.CartDetailId == cartDetailsId);

				int totalCountofCartItem = _db.CartDetails.Where(u => u.CartHeaderId == cartDetail.CartHeaderId).Count();
				_db.CartDetails.Remove(cartDetail);
				//Kullanıcının sepet de sadece bir ürünü varsa 
				if (totalCountofCartItem == 1)
				{
					var cartHeaderToRemove = await _db.CartHeaders
					   .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetail.CartHeaderId);

					_db.CartHeaders.Remove(cartHeaderToRemove);
				}
				await _db.SaveChangesAsync();

				_response.Result = true;
			}
			catch (Exception ex)
			{
				_response.Message = ex.Message.ToString();
				_response.IsSuccess = false;
			}
			return _response;
		}

		[HttpGet("GetCart/{userId}")]
		public async Task<ResponseDto> GetCart(string userId)
		{
			try
			{
				CartDto cart = new()
				{
					CartHeader = _mapper.Map<CartHeaderDto>(_db.CartHeaders.First(u => u.UserId == userId))
				};
				cart.CartDetails = _mapper.Map<IEnumerable<CartDetailDto>>(_db.CartDetails
					.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId));

				//todo: Tüm productlar nerde tutulacak redis gibi hızlı bi yerden 
				IEnumerable<ProductDto> productDtos = await _productService.GetProducts(cart.CartDetails.Select(i => i.ProductId));

				foreach (var item in cart.CartDetails)
				{
					item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
					cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
				}

				//apply coupon if any
				if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
				{
					CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
					if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
					{
						cart.CartHeader.CartTotal -= coupon.DiscountAmount;
						cart.CartHeader.Discount = coupon.DiscountAmount;
					}
				}

				_response.Result = cart;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}
	}
}
