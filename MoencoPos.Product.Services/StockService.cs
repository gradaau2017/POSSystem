using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.DAL.UnitOfWork;
using MoencoPOS.Models;


namespace MoencoPos.Product.Services
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IUnitOfWork unitofWork)
        {
           this._unitOfWork = unitofWork;
        }

        public bool AddStock(Stock stock)
        {
            _unitOfWork.StockRepository.Add(stock);
            _unitOfWork.Save();
            return true;
        }

        public bool AddStockQuantity(int branchId, int productId, int quantity)
        {
            Stock stock = FindByBranchandProduct(branchId, productId);
            if (stock == null)
            {
                stock = new Stock()
                {
                    BranchId=branchId,
                    ProductId=productId,
                    Quantity=quantity,
                };
                return AddStock(stock);
            }
            else
            {
                stock.Quantity = stock.Quantity + quantity;
                return EditStock(stock);
            }
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.StockRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.StockRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteStock(Stock stock)
        {
            if (stock == null) return false;
            _unitOfWork.StockRepository.Delete(stock);
            _unitOfWork.Save();
            return true;
        }

        public bool EditStock(Stock stock)
        {
            _unitOfWork.StockRepository.Edit(stock);
            _unitOfWork.Save();
            return true;
        }

        public List<Stock> FindBy(Expression<Func<Stock, bool>> predicate)
        {
            return _unitOfWork.StockRepository.FindBy(predicate);
        }

        public Stock FindByBranchandProduct(int BranchId, int ProductId)
        {
            return this._unitOfWork.StockRepository.FindBy(x=>x.BranchId==BranchId && x.ProductId==ProductId).SingleOrDefault();
        }

        public Stock FindById(int id)
        {
            return _unitOfWork.StockRepository.FindById(id);
        }

        public IEnumerable<Stock> Get(Expression<Func<Stock, bool>> filter = null, Func<IQueryable<Stock>, IOrderedQueryable<Stock>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.StockRepository.Get(filter, orderBy, includeProperties);
        }

        public List<Stock> GetAllStocks()
        {
            return _unitOfWork.StockRepository.GetAll();
        }

        public bool IsProductQuantityAvailable(int branchId, int productId, int quantity)
        {
            Stock stock = FindByBranchandProduct(branchId, productId);
            if (stock == null)
                return false;
            return (stock.Quantity >= quantity);
        }

        public bool SubtractProduct(int branchId, int productId, int quantity)
        {
            if(IsProductQuantityAvailable( branchId,  productId,  quantity))
            {
                Stock stock = FindByBranchandProduct(branchId, productId);
                stock.Quantity = stock.Quantity - quantity;
                return EditStock(stock);
            }
            return false;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
