using AutoMapper;
using ikifikir.COMMON.DataTransfer.TeamsData;
using ikifikir.editor.Models.TeamsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Profiles
{
    public class TeamsProfile : Profile
    {
        public TeamsProfile()
        {
            CreateMap<TeamsDto, TeamsViewModel>();
            CreateMap<TeamsListItemDto, TeamsListViewModel>();
            CreateMap<TeamsCreateViewModel, TeamsDto>();
            CreateMap<TeamsDto, TeamsEditViewModel>();
            CreateMap<TeamsEditViewModel, TeamsDto>();
        }
    }
}
