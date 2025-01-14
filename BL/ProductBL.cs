﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BL
{
    public class ProductBL
    {
	    public async Task<Guid> AddOrUpdateAsync(Product entity)
	    {
		    entity.Id = await new ProductDal().AddOrUpdateAsync(entity);
		    return entity.Id;
	    }

	    public Task<bool> ExistsAsync(Guid id)
	    {
		    return new ProductDal().ExistsAsync(id);
	    }

	    public async Task<Product> GetAsync(Guid id)
	    {
		    return await new ProductDal().GetAsync(id);
	    }

	    public async Task<List<Product>> GetAsync()
	    {
		    return await new ProductDal().GetAsync();
	    }

	    public async Task<List<Product>> GetAsync(Expression<Func<Product, bool>> predicate)
	    {
		    return await new ProductDal().GetAsync(predicate);
	    }

	    public async Task<List<Product>> GetAsync(Expression<Func<Product, bool>> filter = null, params Expression<Func<Product, object>>[] includes)
	    {
		    return await new ProductDal().GetAsync(filter, includes);
	    }

		public async Task<List<Product>> GetAsync(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, params Expression<Func<Product, object>>[] includes)
	    {
		    return await new ProductDal().GetAsync(filter, orderBy, includes);
	    }

		public Task<bool> DeleteHardAsync(Guid id)
	    {
		    return new ProductDal().DeleteHardAsync(id);
	    }
    }
}
