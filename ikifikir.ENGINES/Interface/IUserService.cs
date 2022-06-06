using ikifikir.COMMON.DataTransfer.UserData;
using ikifikir.CORE.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IUserService
    {
        Task<bool> Register(UserDto model);
        Task<bool> LoginAsync(string email, string password);
        UserDto GetUserByName(string name);
        List<UserListItemDto> UserList();
        UserDto GetUserById(int id);
        bool DeleteUserById(int id);
        Task<bool> UpdateUser(UserDto userDto);
    }
}
