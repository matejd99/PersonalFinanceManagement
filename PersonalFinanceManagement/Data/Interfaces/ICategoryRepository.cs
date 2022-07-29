using PersonalFinanceManagement.Dto;

namespace PersonalFinanceManagement.Data.Interfaces
{
    public interface ICategoryRepository
    {
        
        Task<List<CategoryDto>> ListCategories();
        
    }
}
