using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM.Backend.Models;
using PFM.Backend.Services;
using System.Globalization;

namespace PFM.Backend.Controllers
{
    [Route("transactions")]
    [ApiController]
    public class ImportTransactionsController : ControllerBase
    {
        [HttpPost("import")]
        public List<TransactionsModel> ImportTransactions(IFormFile csv)
        {
            using (var streamReader = new StreamReader(csv.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<TransactionMapper>();

                    var a = csvReader.GetRecords<TransactionsModel>();

                    return a.ToList();
                }
            }
            return new List<TransactionsModel>();
        }
    }
}