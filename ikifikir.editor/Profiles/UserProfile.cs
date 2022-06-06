using AutoMapper;
using ikifikir.COMMON.DataTransfer.RoleData;
using ikifikir.COMMON.DataTransfer.UserData;
using ikifikir.editor.Models.RolModel;
using ikifikir.editor.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserViewModel>();
            CreateMap<UserListItemDto, UserListViewModel>();
            CreateMap<UserCreateViewModel, UserDto>();
            CreateMap<UserDto, UserEditViewModel>();
            CreateMap<UserEditViewModel, UserDto>();

            CreateMap<RoleDto, RolViewModel>();
            CreateMap<RoleListItemDto, RolListViewModel>();
            CreateMap<RolCreateViewModel, RoleDto>();
            CreateMap<RoleDto, RolEditViewModel>();
            CreateMap<RolEditViewModel, RoleDto>();
        }
    }
}
