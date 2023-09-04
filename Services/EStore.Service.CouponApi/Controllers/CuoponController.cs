using AutoMapper;
using Azure;
using EStore.Service.CouponApi.Context;
using EStore.Service.CouponApi.Models;
using EStore.Service.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EStore.Service.CouponApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CouponController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;
		public CouponController(ApplicationDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
			_response = new ResponseDto();
		}

		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				IEnumerable<Coupon> objList = _db.Coupons.ToList();
				_response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				if (_response.ErrorMessages == null) _response.ErrorMessages = new();
				_response.ErrorMessages.Add(ex.Message);
			}
			return _response;
		}

		[HttpGet]
		[Route("{id:int}")]
		public ResponseDto Get(int id)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u => u.CouponId == id);
				_response.Result = _mapper.Map<CouponDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				if (_response.ErrorMessages == null) _response.ErrorMessages = new();
				_response.ErrorMessages.Add(ex.Message);
			}
			return _response;
		}


		[HttpGet]
		[Route("GetByCode/{code}")]
		public ResponseDto GetByCode(string code)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
				_response.Result = _mapper.Map<CouponDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				if (_response.ErrorMessages == null) _response.ErrorMessages = new();
				_response.ErrorMessages.Add(ex.Message);
			}
			return _response;
		}

		[HttpPost]
		public ResponseDto Post([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon obj = _mapper.Map<Coupon>(couponDto);
				_db.Coupons.Add(obj);
				_db.SaveChanges();

				_response.Result = _mapper.Map<CouponDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				if (_response.ErrorMessages == null) _response.ErrorMessages = new();
				_response.ErrorMessages.Add(ex.Message);
			}
			return _response;
		}


		[HttpPut]
		public ResponseDto Update([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon obj = _mapper.Map<Coupon>(couponDto);
				_db.Coupons.Update(obj);
				_db.SaveChanges();

				_response.Result = _mapper.Map<CouponDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				if (_response.ErrorMessages == null) _response.ErrorMessages = new();
				_response.ErrorMessages.Add(ex.Message);
			}
			return _response;
		}

		[HttpDelete("{id}")]
		public ResponseDto Delete(int id)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u => u.CouponId == id);
				_db.Coupons.Remove(obj);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				if (_response.ErrorMessages == null) _response.ErrorMessages = new();
				_response.ErrorMessages.Add(ex.Message);
			}
			return _response;
		}
	}
}
