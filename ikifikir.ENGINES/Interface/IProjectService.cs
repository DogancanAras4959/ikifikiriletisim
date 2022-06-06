using ikifikir.COMMON.DataTransfer.ProjectData;
using ikifikir.COMMON.DataTransfer.TagData;
using ikifikir.COMMON.DataTransfer.TagProjectData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IProjectService
    {
        #region Admin Panel
        Task<bool> insertProject(ProjectDto model);
        Task<bool> updateProject(ProjectDto model);
        bool deleteProject(int id);
        Task<bool> projectTitleShowProcess(int id);
        Task<bool> projectIsActiveProcess(int id);
        ProjectDto getProjectById(int id);
        List<ProjectListItemDto> getProjectList();
        List<ProjectListItemDto> getProjectParent();
        List<ProjectListItemDto> getProjectListByCategoryId(int categoryId);
        List<ProjectListItemDto> searchDataInProject(string searchKey);

        #endregion

        #region Tags

        Task<bool> createTag(TagDto model);
        TagDto getTags(string name);
        bool tagDelete(int id);
        Task InsertTagToProject(string v, int resultId);
        TagDto tagGet(int etiketId);
        List<TagProjectListItemDto> tagsListWithProjectByProjectId(int id);
        
        #endregion

        #region Web

        List<ProjectListItemDto> getProjectListInSlider();

        #endregion
    }
}
