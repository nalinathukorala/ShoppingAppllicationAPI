using System;
using System.Collections.Generic;
using System.Text;
using OnlineShopping.DataAccess.Models;
//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Repository.Interfaces
{
   public interface IAuthRepository<T>: IRepository<T> where T : class
    {
        Task<Customers> Register(Customers user, string password);
        Task<Customers> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
