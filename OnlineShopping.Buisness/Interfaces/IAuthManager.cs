using OnlineShopping.Buisness.DTOs;
using OnlineShopping.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Buisness.Interfaces
{
    public interface IAuthManager
    {
        Task<OperationResult> UserRegister(UserForRegisterDto userForRegisterDto);

        Task<OperationResult> UserLogin(UserForLoginDto userForLoginDto);
    }
}
