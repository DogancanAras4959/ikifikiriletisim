using ikifikir.COMMON.DataTransfer.CategoryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface ICategoryService
    {
        Task<bool> insertCategory(CategoryDto model);
        Task<bool> updateCategory(CategoryDto model);
        bool deleteCategory(int id);
        CategoryDto getCategoryById(int id);
        List<CategoryListItemDto> getCategoryList();
    }
}
