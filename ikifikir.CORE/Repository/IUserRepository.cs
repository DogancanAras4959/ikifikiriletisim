using ikifikir.CORE.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.CORE.Repository
{
    public interface IUserRepository
    {
        Task<TokenResponse> LoginAsync(string email, string password);
    }
}
