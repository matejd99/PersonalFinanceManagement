using CsvHelper.Configuration;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Services
{
    public class CategoriesMapper : ClassMap<CategoriesModel>
    {
        public CategoriesMapper() {
            Map(m => m.Code).Name("code");
            Map(m => m.ParentCode).Name("parent-code");
            Map(m => m.Name).Name("name");
        }
    }
}
