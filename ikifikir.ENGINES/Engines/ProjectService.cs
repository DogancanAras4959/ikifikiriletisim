using ikifikir.COMMON.DataTransfer.ProjectData;
using ikifikir.COMMON.DataTransfer.ReferenceData;
using ikifikir.COMMON.DataTransfer.TagData;
using ikifikir.COMMON.DataTransfer.TagProjectData;
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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public ProjectService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Project

        public bool deleteProject(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<project>().DeleteAsync(new project { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public List<ProjectListItemDto> getProjectList()
        {
            IEnumerable<project> roles = _unitOfWork.GetRepository<project>().Where(null, x => x.OrderBy(y => y.sorted), "category", null, null);

            return roles.Select(x => new ProjectListItemDto
            {
                Id = x.Id,
                projectName = x.projectName,
                seoDescription = x.seoDescription,
                seoTitle = x.seoTitle,
                IsActive = x.IsActive,
                projectSpot = x.projectSpot,
                categoryId = x.categoryId,
                client = x.client,
                parentProjectId = x.parentProjectId,
                isSlider = x.isSlider,
                CreatedTime = x.CreatedTime,
                isTitle = x.isTitle,
                description = x.description,
                imageThumbnail = x.imageThumbnail,
                UpdatedTime = x.UpdatedTime,
                website = x.website,
                category = x.category,

            }).ToList();
        }
        public List<ProjectListItemDto> getProjectListWeb()
        {
            IEnumerable<project> roles = _unitOfWork.GetRepository<project>().Where(x=> x.IsActive == true, x => x.OrderBy(y => y.sorted), "category", null, null);

            return roles.Select(x => new ProjectListItemDto
            {
                Id = x.Id,
                projectName = x.projectName,
                seoDescription = x.seoDescription,
                seoTitle = x.seoTitle,
                IsActive = x.IsActive,
                projectSpot = x.projectSpot,
                categoryId = x.categoryId,
                client = x.client,
                parentProjectId = x.parentProjectId,
                isSlider = x.isSlider,
                CreatedTime = x.CreatedTime,
                isTitle = x.isTitle,
                description = x.description,
                imageThumbnail = x.imageThumbnail,
                UpdatedTime = x.UpdatedTime,
                website = x.website,
                category = x.category,

            }).ToList();
        }
        public List<ProjectListItemDto> getProjectListByCategoryId(int categoryId)
        {
            IEnumerable<project> roles = _unitOfWork.GetRepository<project>().Where(x => x.categoryId == categoryId, x => x.OrderBy(y => y.Id), "category", null, null);

            return roles.Select(x => new ProjectListItemDto
            {
                Id = x.Id,
                projectName = x.projectName,
                seoDescription = x.seoDescription,
                seoTitle = x.seoTitle,
                IsActive = x.IsActive,
                projectSpot = x.projectSpot,
                categoryId = x.categoryId,
                isSlider = x.isSlider,
                parentProjectId = x.parentProjectId,
                client = x.client,
                isTitle = x.isTitle,
                CreatedTime = x.CreatedTime,
                description = x.description,
                imageThumbnail = x.imageThumbnail,
                UpdatedTime = x.UpdatedTime,
                website = x.website,
                category = x.category,

            }).ToList();
        }
        public List<ProjectListItemDto> getProjectListByCategoryIdWeb(int categoryId)
        {
            IEnumerable<project> roles = _unitOfWork.GetRepository<project>().Where(x => x.categoryId == categoryId && x.IsActive == true, x => x.OrderBy(y => y.Id), "category", null, null);

            return roles.Select(x => new ProjectListItemDto
            {
                Id = x.Id,
                projectName = x.projectName,
                seoDescription = x.seoDescription,
                seoTitle = x.seoTitle,
                IsActive = x.IsActive,
                projectSpot = x.projectSpot,
                categoryId = x.categoryId,
                isSlider = x.isSlider,
                parentProjectId = x.parentProjectId,
                client = x.client,
                isTitle = x.isTitle,
                CreatedTime = x.CreatedTime,
                description = x.description,
                imageThumbnail = x.imageThumbnail,
                UpdatedTime = x.UpdatedTime,
                website = x.website,
                category = x.category,

            }).ToList();
        }
        public ProjectDto getProjectById(int id)
        {
            project prj = _unitOfWork.GetRepository<project>().Where(x => x.Id == id, x => x.OrderBy(y => y.Id), "category", null, null).SingleOrDefault();

            if (prj == null)
            {
                return new ProjectDto();
            }

            return new ProjectDto
            {
                description = prj.description,
                IsActive = prj.IsActive,
                CreatedTime = prj.CreatedTime,
                UpdatedTime = prj.UpdatedTime,
                seoDescription = prj.seoDescription,
                seoTitle = prj.seoTitle,
                isSlider = prj.isSlider,
                parentProjectId = prj.parentProjectId,
                sorted = prj.sorted,
                category = prj.category,
                projectSpot = prj.projectSpot,
                isTitle = prj.isTitle,
                categoryId = prj.categoryId,
                client = prj.client,
                imageThumbnail = prj.imageThumbnail,
                projectName = prj.projectName,
                website = prj.website,
                Id = prj.Id
            };
        }
        public async Task<bool> insertProject(ProjectDto model)
        {

            project prj = await _unitOfWork.GetRepository<project>().AddAsync(new project
            {
                description = model.description,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                seoDescription = model.seoDescription,
                seoTitle = model.seoTitle,
                isSlider = model.isSlider,
                sorted = model.sorted,
                projectSpot = model.projectSpot,
                categoryId = model.categoryId,
                client = model.client,
                imageThumbnail = model.imageThumbnail,
                parentProjectId = model.parentProjectId,
                projectName = model.projectName,
                website = model.website,
                isTitle = model.isTitle,
            });

            return prj != null && prj.Id != 0;
        }
        public async Task<bool> updateProject(ProjectDto model)
        {
            project prjGet = await _unitOfWork.GetRepository<project>().FindAsync(x => x.Id == model.Id);

            project getPrj = await _unitOfWork.GetRepository<project>().UpdateAsync(new project
            {
                description = model.description,
                IsActive = prjGet.IsActive,
                CreatedTime = prjGet.CreatedTime,
                UpdatedTime = DateTime.Now,
                seoDescription = model.seoDescription,
                parentProjectId = model.parentProjectId,
                seoTitle = model.seoTitle,
                isSlider = model.isSlider,
                sorted = model.sorted,
                projectSpot = model.projectSpot,
                categoryId = model.categoryId,
                client = model.client,
                imageThumbnail = model.imageThumbnail,
                projectName = model.projectName,
                website = model.website,
                isTitle = model.isTitle,
                Id = model.Id,
            });

            return getPrj != null;
        }
        public List<ProjectListItemDto> searchDataInProject(string searchKey)
        {
            try
            {
                IEnumerable<project> getNews = _unitOfWork.GetRepository<project>().Where(null, x => x.OrderByDescending(y => y.Id), "category", null, null);

                if (!String.IsNullOrEmpty(searchKey))
                {
                    getNews = getNews.Where(x => x.projectName!.Contains(searchKey));
                }

                return getNews.Select(x => new ProjectListItemDto
                {
                    Id = x.Id,
                    CreatedTime = x.CreatedTime,
                    parentProjectId = x.parentProjectId,
                    UpdatedTime = x.UpdatedTime,
                    categoryId = x.categoryId,
                    seoDescription = x.seoDescription,
                    seoTitle = x.seoTitle,
                    isSlider = x.isSlider,
                    sorted = x.sorted,
                    projectSpot = x.projectSpot,
                    category = x.category,
                    client = x.client,
                    description = x.description,
                    imageThumbnail = x.imageThumbnail,
                    IsActive = x.IsActive,
                    projectName = x.projectName,
                    website = x.website,
                    isTitle = x.isTitle,

                }).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<bool> projectTitleShowProcess(int id)
        {
            project getNews = _unitOfWork.GetRepository<project>().FindAsync(x => x.Id == id).Result;
            if (getNews.isTitle == false)
            {
                getNews.isTitle = true;
                project model = await _unitOfWork.GetRepository<project>().UpdateAsync(getNews);
                return getNews != null;
            }
            else
            {
                getNews.isTitle = false;
                project model = await _unitOfWork.GetRepository<project>().UpdateAsync(getNews);
                return getNews != null;
            }
        }
        public async Task<bool> projectIsActiveProcess(int id)
        {
            project getNews = _unitOfWork.GetRepository<project>().FindAsync(x => x.Id == id).Result;
            if (getNews.IsActive == false)
            {
                getNews.IsActive = true;
                project model = await _unitOfWork.GetRepository<project>().UpdateAsync(getNews);
                return getNews != null;
            }
            else
            {
                getNews.IsActive = false;
                project model = await _unitOfWork.GetRepository<project>().UpdateAsync(getNews);
                return getNews != null;
            }
        }
        public List<ProjectListItemDto> getProjectParent()
        {
            IEnumerable<project> roles = _unitOfWork.GetRepository<project>().Where(x => x.parentProjectId == 0, x => x.OrderBy(y => y.Id), "", null, null);

            return roles.Select(x => new ProjectListItemDto
            {
                Id = x.Id,
                projectName = x.projectName,
                seoDescription = x.seoDescription,
                seoTitle = x.seoTitle,
                IsActive = x.IsActive,
                projectSpot = x.projectSpot,
                categoryId = x.categoryId,
                isSlider = x.isSlider,
                parentProjectId = x.parentProjectId,
                client = x.client,
                isTitle = x.isTitle,
                CreatedTime = x.CreatedTime,
                description = x.description,
                imageThumbnail = x.imageThumbnail,
                UpdatedTime = x.UpdatedTime,
                website = x.website,
                category = x.category,

            }).ToList();
        }
        public List<ProjectListItemDto> getProjectListInSlider()
        {
            IEnumerable<project> roles = _unitOfWork.GetRepository<project>().Where(null, x => x.OrderBy(y => y.Id), "category", 0, 10);

            return roles.Select(x => new ProjectListItemDto
            {
                Id = x.Id,
                projectName = x.projectName,
                seoDescription = x.seoDescription,
                seoTitle = x.seoTitle,
                IsActive = x.IsActive,
                projectSpot = x.projectSpot,
                categoryId = x.categoryId,
                isSlider = x.isSlider,
                client = x.client,
                parentProjectId = x.parentProjectId,
                CreatedTime = x.CreatedTime,
                isTitle = x.isTitle,
                description = x.description,
                imageThumbnail = x.imageThumbnail,
                UpdatedTime = x.UpdatedTime,
                website = x.website,
                category = x.category,

            }).ToList();
        }

        #endregion

        #region Tag
        public async Task<bool> createTag(TagDto model)
        {
            tags newTags = await _unitOfWork.GetRepository<tags>().AddAsync(new tags
            {
                name = model.name
            });

            return newTags != null && newTags.Id != 0;
        }
        public TagDto getTags(string name)
        {
            tags getTags = _unitOfWork.GetRepository<tags>().FindAsync(x => x.name == name).Result;

            return new TagDto
            {
                Id = getTags.Id,
                name = getTags.name,
            };
        }
        public bool tagDelete(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<tags>().DeleteAsync(new tags { Id = id });
            return Convert.ToBoolean(result.Result);
        }
        public async Task InsertTagToProject(string v, int resultId)
        {
            project getNews = await _unitOfWork.GetRepository<project>().FindAsync(x => x.Id == resultId);
            string[] listTags = v.Split(',');

            for (int i = 0; i < listTags.Count(); i++)
            {
                if (await _unitOfWork.GetRepository<tags>().FindAsync(x => x.name == listTags[i].Trim().ToString()) != null)
                {
                    //var ise oluşturmayacak
                }
                else
                {
                    tags tags = await _unitOfWork.GetRepository<tags>().AddAsync(new tags
                    {
                        name = listTags[i].Trim().ToString()
                    });
                }
            }

            foreach (string item in listTags) //Çalışmıyor
            {
                string etiketAdi = item.Trim();
                tags etiketiGetir = await _unitOfWork.GetRepository<tags>().FindAsync(x => x.name == etiketAdi);

                if (await _unitOfWork.GetRepository<tagProject>().FindAsync(x => x.projectId == getNews.Id && x.tagId == etiketiGetir.Id) != null)
                {
                    //var ise eklemeyecek
                }
                else
                {
                    tagProject tagNews = await _unitOfWork.GetRepository<tagProject>().AddAsync(new tagProject
                    {
                        projectId = getNews.Id,
                        tagId = etiketiGetir.Id,
                    });
                }
            }

        }
        public TagDto tagGet(int etiketId)
        {
            tags getTags = _unitOfWork.GetRepository<tags>().FindAsync(x => x.Id == etiketId).Result;

            return new TagDto
            {
                Id = getTags.Id,
                name = getTags.name
            };
        }
        public List<TagProjectListItemDto> tagsListWithProjectByProjectId(int id)
        {
            IEnumerable<tagProject> newsList = _unitOfWork.GetRepository<tagProject>().Where(x => x.projectId == id, x => x.OrderByDescending(y => y.Id), "projectToTag,tagToTag", 1, 50);

            if (newsList != null)
            {
                return newsList.Select(x => new TagProjectListItemDto
                {

                    Id = x.Id,
                    projectId = x.projectId,
                    tagId = x.tagId,
                    projectToTag = x.projectToTag,
                    tagToTag = x.tagToTag,

                }).ToList();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Reference Logos

        public List<ReferenceListItemDto> referenceLogos()
        {
            IEnumerable<referenceLogo> roles = _unitOfWork.GetRepository<referenceLogo>().Where(null, x => x.OrderBy(y => y.Id), "", null, null);

            return roles.Select(x => new ReferenceListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,            
                CreatedTime = x.CreatedTime,    
                UpdatedTime = x.UpdatedTime,
                slug = x.slug,
                sorted = x.sorted

            }).ToList();
        }

        public async Task<bool> insertReferenceLogoImage(List<ReferenceListItemDto> model)
        {
            try
            {
                foreach (var item in model)
                {
                    referenceLogo gal = await _unitOfWork.GetRepository<referenceLogo>().AddAsync(new referenceLogo
                    {
                        IsActive = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        slug = item.slug,
                        sorted = item.sorted,
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


    }
}
