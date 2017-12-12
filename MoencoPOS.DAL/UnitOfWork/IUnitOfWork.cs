using System;
using System.Data.Entity;
using MoencoPOS.Models;
using MoencoPOS.DAL.Repository;

namespace MoencoPOS.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // TODO: Add properties to be implemented by UnitOfWork class for each repository

        Database Database { get; }
        IGenericRepository<Address> AddressRepository { get; }
        IGenericRepository<SalesInvoice> SalesInvoiceRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Branch> BranchRepository { get; }
        IGenericRepository<Productc> ProductcRepository { get; }



        void Save();

    }
}
