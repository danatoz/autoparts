﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceParseServices
{
	public class UploadedModel
	{
		public Guid PriceId { get; set; }
		public List<PriceModel> Price { get; set; }
	}
}
