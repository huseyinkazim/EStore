using AutoMapper;
using EStore.Service.ProductApi.Context;
using EStore.Service.ProductApi.Models;
using EStore.Service.ProductApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EStore.Service.ProductApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;
		public ProductController(ApplicationDbContext db, IMapper mapper)
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
				IEnumerable<Product> objList = _db.Products.ToList();
				_response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = ex.Message;
			}
			return _response;
		}

		[HttpGet]
		[Route("{id:int}")]
		public ResponseDto Get(int id)
		{
			try
			{
				Product obj = _db.Products.First(u => u.ProductId == id);
				_response.Result = _mapper.Map<ProductDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = ex.Message;
			}
			return _response;
		}

		[HttpPost("GetByIds")]
		[Authorize(Roles = "admin")]
		public ResponseDto GetByIds([FromBody] IEnumerable<int> ids)
		{
			try
			{
				IEnumerable<Product> objs = _db.Products.Where(u => ids.Contains(u.ProductId));
				_response.Result = _mapper.Map<IEnumerable<ProductDto>>(objs);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = ex.Message;
			}
			return _response;
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public ResponseDto Post([FromBody] ProductDto ProductDto)
		{
			try
			{
				Product obj = _mapper.Map<Product>(ProductDto);
				_db.Products.Add(obj);
				_db.SaveChanges();

				_response.Result = _mapper.Map<ProductDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = ex.Message;
			}
			return _response;
		}


		[HttpPut]
		[Authorize(Roles = "admin")]
		public ResponseDto Put([FromBody] ProductDto ProductDto)
		{
			try
			{
				Product obj = _mapper.Map<Product>(ProductDto);
				_db.Products.Update(obj);
				_db.SaveChanges();

				_response.Result = _mapper.Map<ProductDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = ex.Message;
			}
			return _response;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]
		public ResponseDto Delete(int id)
		{
			try
			{
				Product obj = _db.Products.First(u => u.ProductId == id);
				_db.Products.Remove(obj);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = ex.Message;
			}
			return _response;
		}
	}
}
