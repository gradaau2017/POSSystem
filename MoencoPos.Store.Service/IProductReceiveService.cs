using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.Models;

namespace MoencoPos.Store.Service
{
    public interface IProductReceiveService : IDisposable
    {
        bool AddProductReceiveInvoice(ProductReceiveInvoice productReceiveInvoice);
        bool EditProductReceiveInvoice(ProductReceiveInvoice productReceiveInvoice);
        bool DeleteProductReceiveInvoice(ProductReceiveInvoice productReceiveInvoice);
        bool DeleteProductReceiveInvoiceById(int id);
        ProductReceiveInvoice FindById(int id);
        List<ProductReceiveInvoice> GetAllProductReceiveInvoices();
        List<ProductReceiveInvoice> FindBy(Expression<Func<ProductReceiveInvoice, bool>> predicate);
        IEnumerable<ProductReceiveInvoice> Get(
                   Expression<Func<ProductReceiveInvoice, bool>> filter = null,
                   Func<IQueryable<ProductReceiveInvoice>, IOrderedQueryable<ProductReceiveInvoice>> orderBy = null,
                   string includeProperties = "");

    }
}
