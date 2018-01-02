using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.Models;
using MoencoPOS.DAL.UnitOfWork;

namespace MoencoPos.Store.Services
{
    public class ProductReceiveService : IProductReceiveService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductReceiveService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddProductReceive(ProductReceive productReceive)
        {
            _unitOfWork.ProductReceiveRepository.Add(productReceive);
            if (productReceive.ProductReceiveLineItems != null)
            {
                foreach (var item in productReceive.ProductReceiveLineItems)
                {
                    AddLineItemStock(item, productReceive.BranchId);
                }
            }
            _unitOfWork.Save();
            return true;
        }

        void AddLineItemStock(ProductReceiveLineItem item, int branchId)
        {
            var stock = _unitOfWork.StockRepository.FindBy(x => x.BranchId == branchId
                                                                && x.ProductId == item.ProductId).SingleOrDefault();
            if (stock == null)
            {
                stock = new Stock()
                {
                    BranchId = branchId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                _unitOfWork.StockRepository.Add(stock);
            }
            else
            {
                stock.Quantity = stock.Quantity + item.Quantity;
                _unitOfWork.StockRepository.Edit(stock);
            }
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ProductReceiveRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ProductReceiveRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteProductReceive(ProductReceive productReceive)
        {
            if (productReceive == null) return false;
            _unitOfWork.ProductReceiveRepository.Delete(productReceive);
            _unitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public bool EditProductReceive(ProductReceive productReceive)
        {
            _unitOfWork.ProductReceiveRepository.Edit(productReceive);
            _unitOfWork.Save();
            return true;
        }

        public List<ProductReceive> FindBy(Expression<Func<ProductReceive, bool>> predicate)
        {
            return _unitOfWork.ProductReceiveRepository.FindBy(predicate);
        }

        public ProductReceive FindById(int id)
        {
            return _unitOfWork.ProductReceiveRepository.FindById(id);
        }

        public IEnumerable<ProductReceive> Get(Expression<Func<ProductReceive, bool>> filter = null, Func<IQueryable<ProductReceive>, IOrderedQueryable<ProductReceive>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ProductReceiveRepository.Get(filter, orderBy, includeProperties);
        }

        public List<ProductReceive> GetAllProductReceive()
        {
            return _unitOfWork.ProductReceiveRepository.GetAll();
        }
    }
}
