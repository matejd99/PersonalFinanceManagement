using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Services.Interfaces
{
    public interface ICategoryInterface
    {
        Task<CategoryDto> GetCategoryById(int id);
        Task<List<CategoriesModel>> ImportCategoriesAsync(IFormFile csv)

    }
}
