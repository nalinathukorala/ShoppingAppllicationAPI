using OnlineShopping.DataAccess.Models;
using OnlineShopping.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopping.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShoppingContext _context;

        public IAuthRepository<Customers> Customers { get; private set; }
        public UnitOfWork(OnlineShoppingContext context)
        {
            _context = context;
            Customers = new AuthRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int saveChanges()
        {
          return  _context.SaveChanges();
        }
    }
}
