using AutoMapper;
using ikifikir.COMMON.DataTransfer.TeamsData;
using ikifikirweb.ViewModels.TeamModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.Profiles
{
    public class TeamProfiles : Profile
    {
        public TeamProfiles()
        {
            CreateMap<TeamsListItemDto, TeamListViewModel>();
        }
    }
}
