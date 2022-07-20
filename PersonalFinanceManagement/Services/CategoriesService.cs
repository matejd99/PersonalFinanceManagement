using CsvHelper;
using PersonalFinanceManagement.Data;
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

        public List<CategoriesModel> ImportCategories(IFormFile csv)
        {
            using (var streamReader = new StreamReader(csv.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<CategoriesMapper>();
                    var categories = csvReader.GetRecords<CategoriesModel>();
                    Context.Categories.AddRange(categories);
                    return categories.ToList();

                }
            }
            return new List<CategoriesModel>();
        }

    }
}