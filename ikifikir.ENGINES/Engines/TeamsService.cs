using ikifikir.COMMON.DataTransfer.TeamsData;
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
    public class TeamsService : ITeamsService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public TeamsService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool deleteTeam(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<teams>().DeleteAsync(new teams { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public TeamsDto getTeamById(int id)
        {
            teams team = _unitOfWork.GetRepository<teams>().FindAsync(x => x.Id == id).Result;

            if (team == null)
            {
                return new TeamsDto();
            }

            return new TeamsDto
            {
                name = team.name,
                IsActive = team.IsActive,
                CreatedTime = team.CreatedTime,
                UpdatedTime = team.UpdatedTime,
                image = team.image,
                instagram = team.instagram,
                facebook = team.facebook,
                gmail = team.gmail,
                role = team.role,
                twitter = team.twitter,
                Id = team.Id
            };
        }

        public List<TeamsListItemDto> getTeamList()
        {
            IEnumerable<teams> roles = _unitOfWork.GetRepository<teams>().Where(null, x => x.OrderBy(y => y.Id), "", 1, 30);

            return roles.Select(x => new TeamsListItemDto
            {
                Id = x.Id,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                facebook = x.facebook,
                twitter = x.twitter,
                image = x.image,
                instagram = x.instagram,
                gmail = x.gmail,
                IsActive = x.IsActive,
                name = x.name,
                role = x.role,

            }).ToList();
        }

        public async Task<bool> insertTeam(TeamsDto model)
        {
            teams newRole = await _unitOfWork.GetRepository<teams>().AddAsync(new teams
            {
                CreatedTime = DateTime.Now,
                facebook = model.facebook,
                gmail = model.gmail,
                image = model.image,
                instagram = model.image,
                IsActive = true,
                name = model.name,
                role = model.role,
                twitter = model.twitter,
                UpdatedTime = DateTime.Now,
            });

            return newRole != null && newRole.Id != 0;
        }

        public async Task<bool> updateTeam(TeamsDto model)
        {
            teams roleGet = await _unitOfWork.GetRepository<teams>().FindAsync(x => x.Id == model.Id);

            if (model.image == null)
            {
                model.image = roleGet.image;
            }

            teams getRole = await _unitOfWork.GetRepository<teams>().UpdateAsync(new teams
            {
                CreatedTime = roleGet.CreatedTime,
                facebook = model.facebook,
                gmail = model.gmail,
                image = model.image,
                instagram = model.instagram,
                IsActive = roleGet.IsActive,
                name = model.name,
                role = model.role,
                twitter = model.twitter,
                UpdatedTime = DateTime.Now,
                Id = model.Id,
            });

            return getRole != null;
        }
    }
}
