﻿using System.ComponentModel.DataAnnotations;

namespace EStore.Service.CouponApi.Models
{
	public class Coupon
	{
		[Key]
		public int CouponId { get; set; }
		[Required]
		public string CouponCode { get; set; }
		[Required]
		public double DiscountAmount { get; set; }
		public int MinAmount { get; set; }
	}
}