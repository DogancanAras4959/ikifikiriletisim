using ikifikir.COMMON.DataTransfer.TeamsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface ITeamsService
    {
        Task<bool> insertTeam(TeamsDto model);
        Task<bool> updateTeam(TeamsDto model);
        bool deleteTeam(int id);
        TeamsDto getTeamById(int id);
        List<TeamsListItemDto> getTeamList();
    }
}
