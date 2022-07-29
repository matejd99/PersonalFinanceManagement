using PersonalFinanceManagement.Data.Interfaces;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Factories;

namespace PersonalFinanceManagement.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly PFMDbContext Context;

        public CategoryRepository(PFMDbContext Context)
        {
            this.Context = Context;
        }

        
        public async Task<List<CategoryDto>> ListCategories()
        {
            return Context.Categories.ToList().
                Select(c => CategoryFactory.ToDto(c)).ToList();
        }


    }
}
