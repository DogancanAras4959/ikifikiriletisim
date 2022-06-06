using ikifikir.COMMON.DataTransfer.RoleData;
using ikifikir.COMMON.DataTransfer.RoleData.RoleUserData;
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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public RoleService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool deleteRole(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<roles>().DeleteAsync(new roles { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public RoleDto getRoleById(int id)
        {
            roles role = _unitOfWork.GetRepository<roles>().FindAsync(x => x.Id == id).Result;
            if (role == null)
            {
                return new RoleDto();
            }

            return new RoleDto
            {
                roleName = role.roleName,
                IsActive = role.IsActive,
                CreatedTime = role.CreatedTime,
                UpdatedTime = role.UpdatedTime,
                Id = role.Id
            };
        }

        public List<RoleUserListItemDto> ListRoleUserByUser(int accountId)
        {
            IEnumerable<roleUsers> roles = _unitOfWork.GetRepository<roleUsers>().Where(x => x.userId == accountId, x => x.OrderBy(y => y.Id), "", 1, 30);

            return roles.Select(x => new RoleUserListItemDto
            {
                Id = x.Id,
                userId = x.userId,
                roleId = x.roleId,

            }).ToList();
        }

        public async Task<bool> insertRole(RoleDto model)
        {
            roles newRole = await _unitOfWork.GetRepository<roles>().AddAsync(new roles
            {
                roleName = model.roleName,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                IsActive = true,
            });

            return newRole != null && newRole.Id != 0;
        }

        public List<RoleListItemDto> roleList()
        {
            IEnumerable<roles> roles = _unitOfWork.GetRepository<roles>().Where(null, x => x.OrderBy(y => y.Id), "", 1, 30);

            return roles.Select(x => new RoleListItemDto
            {
                Id = x.Id,
                roleName = x.roleName,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime

            }).ToList();
        }

        public async Task<bool> updateRole(RoleDto model)
        {
            roles roleGet = await _unitOfWork.GetRepository<roles>().FindAsync(x => x.Id == model.Id);

            roles getRole = await _unitOfWork.GetRepository<roles>().UpdateAsync(new roles
            {
                Id = model.Id,
                roleName = model.roleName,
                UpdatedTime = DateTime.Now,
                CreatedTime = roleGet.CreatedTime,
                IsActive = roleGet.IsActive
            });

            return getRole != null;
        }

        public async Task<bool> addRoleToUser(int userId, int roleId)
        {
            roleUsers role = await _unitOfWork.GetRepository<roleUsers>().FindAsync(x => x.roleId == roleId && x.userId == userId);

            if (role == null)
            {
                roleUsers newRole = await _unitOfWork.GetRepository<roleUsers>().AddAsync(new roleUsers
                {
                    roleId = roleId,
                    userId = userId,
                });

                return newRole != null && newRole.Id != 0;
            }
            return false;
        }

        public async Task<bool> removeRoleToUser(int userId, int roleId)
        {
            roleUsers role = await _unitOfWork.GetRepository<roleUsers>().FindAsync(x => x.roleId == roleId && x.userId == userId);

            if (role != null)
            {
                Task<int> result = _unitOfWork.GetRepository<roleUsers>().DeleteAsync(new roleUsers { Id = role.Id });
                return Convert.ToBoolean(result.Result);
            }
            else
            {
                return false;
            }

        }
    }
}
