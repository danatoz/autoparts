﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbModels
{
	public class ProductTemplate
	{
		[Key]
		public string VendorCode { get; set; }

		public string Name { get; set; }

		public int CategoryId { get; set; }

		public int ManufacturerId { get; set; }

		public string Description { get; set; }

		public int ManufacturerType { get; set; }

		public bool Active { get; set; }

		[ForeignKey("ManufacturerId")]
		public Manufacturer Manufacturer { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; }

		public List<ProductPhoto> Photos { get; set; }
	}
}
