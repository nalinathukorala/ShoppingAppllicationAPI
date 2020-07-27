using OnlineShopping.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopping.DataAccess.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository<Customers> Customers { get;}

        int saveChanges();
    }
}
