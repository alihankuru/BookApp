using BookApp.DtoLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookApp.DtoLayer.User.ServiceResponses;

namespace BookApp.BusinessLayer.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDto userDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDto);

    }
}
