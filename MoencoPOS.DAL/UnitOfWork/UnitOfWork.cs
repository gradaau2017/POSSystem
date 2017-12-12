using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Web;
using MoencoPOS.Models;
using MoencoPOS.DAL.Repository;
using System.Text;
using log4net;

namespace MoencoPOS.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoencoPOSContext _context;
        public UnitOfWork()
        {
            this._context = new MoencoPOSContext();
        }

        public UnitOfWork(ILog log)
        {
            this._context = new MoencoPOSContext();
            _log = log;
        }

        public Database Database { get { return _context.Database; } }

        public IGenericRepository<Address> addressRepository;
        public IGenericRepository<Address> AddressRepository
        {
            get { return this.addressRepository ?? (this.addressRepository = new GenericRepository<Address>(_context)); }
        }
        public IGenericRepository<SalesInvoice> salesInvoiceRepository;
        public IGenericRepository<SalesInvoice> SalesInvoiceRepository
        {
            get { return this.salesInvoiceRepository ?? (this.salesInvoiceRepository = new GenericRepository<SalesInvoice>(_context)); }
        }

        public IGenericRepository<Category> categoryRepository;
        public IGenericRepository<Category> CategoryRepository
        {
            get { return this.categoryRepository ?? (this.categoryRepository = new GenericRepository<Category>(_context)); }
        }

        public IGenericRepository<Branch> branchRepository;
        public IGenericRepository<Branch> BranchRepository
        {
            get { return this.branchRepository ?? (this.branchRepository = new GenericRepository<Branch>(_context)); }
        }

        public IGenericRepository<Productc> productcRepository;
        public IGenericRepository<Productc> ProductcRepository
        {
            get { return this.productcRepository ?? (this.productcRepository = new GenericRepository<Productc>(_context)); }
        }

        private readonly ILog _log;
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                for (var eCurrent = e; eCurrent != null; eCurrent = (DbEntityValidationException)eCurrent.InnerException)
                {
                    foreach (var eve in eCurrent.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                        StringBuilder errorMsg = new StringBuilder(String.Empty);
                        var s = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        errorMsg.Append(s);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            errorMsg.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                            _log.Error(errorMsg, eCurrent.GetBaseException());
                        }
                    }
                }
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
