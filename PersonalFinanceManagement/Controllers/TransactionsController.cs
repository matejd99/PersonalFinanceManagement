using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Services;

namespace PersonalFinanceManagement.Controllers
{
    [Route("transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private TransactionsService TransactionsService;

        public TransactionsController(TransactionsService transactionsService)
        {
            TransactionsService = transactionsService;
        }

        [HttpPost("import")]
        public List<TransactionsModel> ImportTransactions(IFormFile csv)
        {
            return TransactionsService.Import(csv);
        }

        [HttpGet("Transactions")]
        public List<TransactionDto> TransactionsGetList([FromQuery] string transactionKind,
                                                        [FromQuery] DateTime? startDate,
                                                        [FromQuery] DateTime? endDate,
                                                        [FromQuery] int? page,
                                                        [FromQuery] int? pageSize)
        {
            return TransactionsService.GetList(transactionKind, startDate, endDate, page, pageSize);
        }
    }
}