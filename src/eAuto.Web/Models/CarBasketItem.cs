﻿using System.ComponentModel.DataAnnotations;

namespace eAuto.Web.Models
{
	public class CarBasketItem
	{
		public CarViewModel Car{ get; set; }
		[Range(1, 1000, ErrorMessage = "Please, enter a value beetween 1 and 1000")]
		public int Count { get; set; }
	}
}