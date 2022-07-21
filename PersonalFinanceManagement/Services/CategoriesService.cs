using CsvHelper;
using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Factories;
using PersonalFinanceManagement.Models;
using System.Globalization;

namespace PersonalFinanceManagement.Services
{
    public class CategoriesService
    {
        private PFMDbContext Context;

        public CategoriesService(PFMDbContext context)
        {
            Context = context;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            CategoriesModel category = await Context.Categories.FindAsync(id);
            if (category != null)
            {
                return CategoryFactory.ToDto(category);
            }
            else return null;
        }

        public async Task<List<CategoriesModel>> ImportCategoriesAsync(IFormFile csv)
        {
            using (var streamReader = new StreamReader(csv.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<CategoriesMapper>();
                    var categories = csvReader.GetRecords<CategoriesModel>();
                    await Context.Categories.AddRangeAsync(categories);
                    await Context.SaveChangesAsync();
                    return categories.ToList();
                }
            }
            return new List<CategoriesModel>();
        }
    }
}