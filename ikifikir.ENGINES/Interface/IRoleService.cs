using ikifikir.COMMON.DataTransfer.RoleData;
using ikifikir.COMMON.DataTransfer.RoleData.RoleUserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IRoleService
    {
        List<RoleListItemDto> roleList();
        Task<bool> insertRole(RoleDto model);
        RoleDto getRoleById(int id);
        Task<bool> updateRole(RoleDto model);
        bool deleteRole(int id);
        Task<bool> addRoleToUser(int userId, int roleId);
        Task<bool> removeRoleToUser(int userId, int roleId);
        List<RoleUserListItemDto> ListRoleUserByUser(int accountId);
    }
}
