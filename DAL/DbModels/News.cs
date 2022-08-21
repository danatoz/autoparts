﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbModels
{
    public class News
    {
	    public int Id { get; set; }
	    public string Title { get; set; }
	    public string Url { get; set; }
	    public string Content { get; set; }
	    public DateTime CreationDate { get; set; }
    }
}
