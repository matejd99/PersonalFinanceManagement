﻿using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Models.Helpers;
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
        public async Task<List<TransactionsModel>> ImportTransactionsAsync(IFormFile csv)
        {
            return await TransactionsService.ImportAsync(csv);
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

        [HttpPost("{id}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] int id, [FromBody] CategorizeRequest request)
        {
            var result = await TransactionsService.CategorizeTransaction(id, request);
            
            if(result == null)
            {
                return NotFound();
            }

            else return Ok(result);
        }

        [HttpGet("spending-analytics")]
        public async Task<GroupCategories> SpendingAnalytics([FromQuery]string CategoryCode, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? direction)
        {
            return await TransactionsService.SpendingAnalytics(CategoryCode, startDate, endDate, direction);
        }

        [HttpPost("{id}/split")]
        public async Task<TransactionDto> SplitTransaction([FromRoute] int id) {
            return null;
        }

    }
}