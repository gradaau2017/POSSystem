﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.Models;

namespace MoencoPos.Store.Services
{
    public interface IProductReceiveService : IDisposable
    {
        bool AddProductReceive(ProductReceive productReceive);
        bool DeleteProductReceive(ProductReceive productReceive);
        bool DeleteById(int id);
        bool EditProductReceive(ProductReceive productReceive);
        ProductReceive FindById(int id);
        List<ProductReceive> GetAllProductReceive();
        List<ProductReceive> FindBy(Expression<Func<ProductReceive, bool>> predicate);
        IEnumerable<ProductReceive> Get(
                   Expression<Func<ProductReceive, bool>> filter = null,
                   Func<IQueryable<ProductReceive>, IOrderedQueryable<ProductReceive>> orderBy = null,
                   string includeProperties = "");
    }
}