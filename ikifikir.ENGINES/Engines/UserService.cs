using ikifikir.COMMON.DataTransfer.UserData;
using ikifikir.CORE.Helper;
using ikifikir.CORE.Helper.Cyrptography;
using ikifikir.CORE.Repository;
using ikifikir.CORE.UnitOfWork;
using ikifikir.DAL;
using ikifikir.DAL.Models;
using ikifikir.ENGINES.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Engines
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        //private readonly IUserRepository _userRepository;
        public UserService(/*IUserRepository userRepository*/ IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_userRepository = userRepository;
        }

        public bool DeleteUserById(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<user>().DeleteAsync(new user { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public UserDto GetUserById(int id)
        {
            user user = _unitOfWork.GetRepository<user>().FindAsync(x => x.Id == id).Result;
            if (user == null)
            {
                return new UserDto();
            }

            return new UserDto
            {
                UserName = user.UserName,
                IsActive = user.IsActive,
                CreatedTime = user.CreatedTime,
                UpdatedTime = user.UpdatedTime,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                Id = user.Id
            };
        }

        public UserDto GetUserByName(string name)
        {
            user userGet = _unitOfWork.GetRepository<user>().FindAsync(x => x.Email == name).Result;
            string passwordDeCrypto = new Cyrpto().TryDecrypt(userGet.Password);

            if (userGet == null)
            {
                return new UserDto();
            }

            return new UserDto
            {
                UserName = userGet.UserName,
                FirstName = userGet.FirstName,
                Id = userGet.Id,
                CreatedTime = userGet.CreatedTime,
                UpdatedTime = userGet.UpdatedTime,
                IsActive = userGet.IsActive,
                Password = passwordDeCrypto,
                LastName = userGet.LastName,
                Email = userGet.Email,
            };
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            user user = await _unitOfWork.GetRepository<user>().FindAsync(x => x.Email == email && x.Password == password);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return await _userRepository.LoginAsync(email, password);
        }

        public async Task<bool> Register(UserDto model)
        {
            model.Password = new Cyrpto().TryEncrypt(model.Password);

            user newUser = await _unitOfWork.GetRepository<user>().AddAsync(new user
            {
                FirstName = model.FirstName,
                IsActive = model.IsActive,
                Password = model.Password,
                LastName = model.LastName,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Email = model.Email,
                UserName = model.UserName
            });

            return newUser != null && newUser.Id != 0;
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {
            user userGet = await _unitOfWork.GetRepository<user>().FindAsync(x => x.Id == userDto.Id);
            string passwordCrypto = "";

            if (userDto.Password != null)
            {
                passwordCrypto = new Cyrpto().TryEncrypt(userDto.Password);
            }
            else
            {
                passwordCrypto = userGet.Password;
            }  

            user getUser = await _unitOfWork.GetRepository<user>().UpdateAsync(new user
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                Password = passwordCrypto,
                UpdatedTime = DateTime.Now,
                CreatedTime = userDto.CreatedTime,
                IsActive = userGet.IsActive,
                Email = userGet.Email,
                LastName = userGet.LastName
            });

            return getUser != null;
        }

        public List<UserListItemDto> UserList()
        {
            IEnumerable<user> roles = _unitOfWork.GetRepository<user>().Where(null, x => x.OrderBy(y => y.Id), "", 1, 30);

            return roles.Select(x => new UserListItemDto
            {
                UserName = x.UserName,
                UpdatedTime = x.UpdatedTime,
                CreatedTime = x.CreatedTime,
                Id = x.Id,
                Email = x.Email,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Password = x.Password,
                IsActive = x.IsActive

            }).ToList();
        }
    }
}
