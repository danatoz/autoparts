﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbModels
{
	public class Catalog : IDbModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string IconUrl { get; set; }
		public int ParentId { get; set; }
		[ForeignKey("ParentId")]
		public Catalog ParentCatalog { get; set; }
		public List<Part> Parts { get; set; }
	}
}
