using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Services;
using System.Globalization;

namespace PersonalFinanceManagement.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpPost("import")]
        public List<CategoriesModel> ImportCategories(IFormFile csv)
        {
            using (var streamReader = new StreamReader(csv.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<CategoriesMapper>();
                    var a = csvReader.GetRecords<CategoriesModel>();
                    return a.ToList();
                }
            }
            return new List<CategoriesModel>();
        }
    }
}