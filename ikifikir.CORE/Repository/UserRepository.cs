using Flurl;
using Flurl.Http;
using ikifikir.COMMON.DataTransfer.UserData;
using ikifikir.CORE.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.CORE.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<TokenResponse> LoginAsync(string email, string password)
        {
            var userLogin = new UserDto
            {
                Email = email,
                Password = password
            };

            return await "https://localhost:44328"
                     .AppendPathSegment("/api/token")
                     .PostJsonAsync(userLogin).ReceiveJson<TokenResponse>();
        }
    }
}
