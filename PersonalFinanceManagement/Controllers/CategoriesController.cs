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

        private readonly CategoriesService categoriesService;  

        public CategoriesController(CategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpPost("import")]
        public async Task<List<CategoriesModel>> ImportCategories(IFormFile csv)
        {
            return await categoriesService.ImportCategoriesAsync(csv);
        }
    }
}