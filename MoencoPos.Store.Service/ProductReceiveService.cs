using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoencoPOS.DAL.UnitOfWork;
using MoencoPOS.Models;
using MoencoPos.Product.Services;

namespace MoencoPos.Store.Service
{
    public class ProductReceiveService : IProductReceiveService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductReceiveService(IUnitOfWork unitofWork)
        {
            this._unitOfWork = unitofWork;
        }
        public bool AddProductReceiveInvoice(ProductReceiveInvoice productReceiveInvoice)
        {
            _unitOfWork.ProductReceiveInvoiceRepository.Add(productReceiveInvoice);

            foreach (var item in productReceiveInvoice.ProductReceiveLineItems)
            {
                AddLineItemStock(item, productReceiveInvoice.BranchId);
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

        public bool DeleteProductReceiveInvoice(ProductReceiveInvoice productReceiveInvoice)
        {
            if (productReceiveInvoice == null) return false;
            _unitOfWork.ProductReceiveInvoiceRepository.Delete(productReceiveInvoice);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteProductReceiveInvoiceById(int id)
        {
            var entity = _unitOfWork.ProductReceiveInvoiceRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ProductReceiveInvoiceRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditProductReceiveInvoice(ProductReceiveInvoice productReceiveInvoice)
        {
            _unitOfWork.ProductReceiveInvoiceRepository.Edit(productReceiveInvoice);
            _unitOfWork.Save();
            return true;
        }

        public List<ProductReceiveInvoice> FindBy(Expression<Func<ProductReceiveInvoice, bool>> predicate)
        {
            return _unitOfWork.ProductReceiveInvoiceRepository.FindBy(predicate);
        }

        public ProductReceiveInvoice FindById(int id)
        {
            return _unitOfWork.ProductReceiveInvoiceRepository.FindById(id);
        }

        public IEnumerable<ProductReceiveInvoice> Get(Expression<Func<ProductReceiveInvoice, bool>> filter = null, Func<IQueryable<ProductReceiveInvoice>, IOrderedQueryable<ProductReceiveInvoice>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ProductReceiveInvoiceRepository.Get(filter, orderBy, includeProperties);
        }

        public List<ProductReceiveInvoice> GetAllProductReceiveInvoices()
        {
            return _unitOfWork.ProductReceiveInvoiceRepository.GetAll();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
