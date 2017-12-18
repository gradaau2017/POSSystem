using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.Models;

namespace MoencoPos.Product.Services
{
    public interface IStockService:IDisposable
    {
        bool AddStock(Stock stock);
        bool DeleteStock(Stock stock);
        bool DeleteById(int id);
        bool EditStock(Stock stock);
        Stock FindById(int id);
        Stock FindByBranchandProduct(int BranchId, int ProductId);
        List<Stock> GetAllStocks();
        List<Stock> FindBy(Expression<Func<Stock, bool>> predicate);
        IEnumerable<Stock> Get(
                   Expression<Func<Stock, bool>> filter = null,
                   Func<IQueryable<Stock>, IOrderedQueryable<Stock>> orderBy = null,
                   string includeProperties = "");

        bool AddStockQuantity(int branchId, int productId, int quantity);

        bool SubtractProduct(int branchId, int productId, int quantity);

        bool IsProductQuantityAvailable(int branchId, int productId, int quantity);

    }
}
