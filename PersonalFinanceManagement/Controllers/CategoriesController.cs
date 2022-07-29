using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Data.Repository;
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
        private readonly CategoryRepository categoryRepository;

        public CategoriesController(CategoriesService categoriesService, CategoryRepository categoryRepository)
        {
            this.categoriesService = categoriesService;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCategories(IFormFile csv)
        {
            var result = await categoriesService.ImportCategoriesAsync(csv);
            if (result == null)
            {
                return NotFound();
            }
            else return Ok(result); 
        }

        [HttpGet]
        public async Task<IActionResult> ListCategories()
        {
            var result = await categoryRepository.ListCategories();
            if(result == null)
            {
                return NotFound();
            } else return Ok(result);
        }


    }
}