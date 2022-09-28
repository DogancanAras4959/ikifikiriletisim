using ikifikir.COMMON.DataTransfer.CategoryData;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public CategoryService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool deleteCategory(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<category>().DeleteAsync(new category { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public CategoryDto getCategoryById(int id)
        {
            category category = _unitOfWork.GetRepository<category>().FindAsync(x => x.Id == id).Result;
            if (category == null)
            {
                return new CategoryDto();
            }

            return new CategoryDto
            {
                name = category.name,
                IsActive = category.IsActive,
                CreatedTime = category.CreatedTime,
                UpdatedTime = category.UpdatedTime,
                filterType = category.filterType,
                categoryTags = category.categoryTags,
                Id = category.Id
            };
        }

        public List<CategoryListItemDto> getCategoryList()
        {
            IEnumerable<category> roles = _unitOfWork.GetRepository<category>().Where(null, x => x.OrderBy(y => y.Id), "", 1, 30);

            return roles.Select(x => new CategoryListItemDto
            {
                Id = x.Id,
                name = x.name,
                UpdatedTime = x.UpdatedTime,
                CreatedTime = x.CreatedTime,
                IsActive = x.IsActive,
                categoryTags = x.categoryTags,
                filterType = x.filterType,
            }).ToList();
        }

        public async Task<bool> insertCategory(CategoryDto model)
        {
            category newRole = await _unitOfWork.GetRepository<category>().AddAsync(new category
            {
                name = model.name,
                CreatedTime = DateTime.Now,
                categoryTags = model.categoryTags,
                UpdatedTime = DateTime.Now,
                IsActive = true,
                filterType = model.filterType,
            });

            return newRole != null && newRole.Id != 0;
        }

        public async Task<bool> updateCategory(CategoryDto model)
        {
            category category = await _unitOfWork.GetRepository<category>().FindAsync(x => x.Id == model.Id);

            category getRole = await _unitOfWork.GetRepository<category>().UpdateAsync(new category
            {
                Id = model.Id,
                name = model.name,
                UpdatedTime = DateTime.Now,
                CreatedTime = category.CreatedTime,
                IsActive = category.IsActive,
                categoryTags = model.categoryTags,
                filterType = model.filterType,

            });

            return getRole != null;
        }
    }
}
