using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Factories
{
    public class CategoryFactory
    {
        public static CategoryDto ToDto(CategoriesModel category)
        {
            return new CategoryDto()
            {
                Code = category.Code,
                ParentCode = category.ParentCode,
                Name = category.Name
            };
        }
    }
}
