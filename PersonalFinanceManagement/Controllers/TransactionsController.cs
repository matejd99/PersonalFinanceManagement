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

        [HttpGet]
        public async Task<IActionResult> TransactionsGetList([FromQuery] string transactionKind,
                                                        [FromQuery] DateTime? startDate,
                                                        [FromQuery] DateTime? endDate,
                                                        [FromQuery] int? page,
                                                        [FromQuery] int? pageSize)
        {
            var result = await TransactionsService.GetList(transactionKind, startDate, endDate, page, pageSize);
            if (result == null)
            {
                return NotFound();
            } else return Ok(result);
        }

        [HttpPost("{id}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] int id, [FromBody] CategorizeRequest request)
        {
            var result = await TransactionsService.CategorizeTransaction(id, request);

            if (result == null)
            {
                return NotFound();
            }

            else return Ok(result);
        }

        [HttpGet("spending-analytics")]
        public async Task<IActionResult> SpendingAnalytics([FromQuery] string CategoryCode, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? direction)
        {
            var result = await TransactionsService.SpendingAnalytics(CategoryCode, startDate, endDate, direction);
            if (result == null)
            {
                return NotFound();
            }
            else return Ok(result); 
        }

        [HttpPost("{id}/split")]
        public async Task<IActionResult> SplitTransaction([FromRoute] int id, [FromBody] List<string> codes) {
           var result = await TransactionsService.SplitTransaction(id, codes);
            if (result == null)
            {
                return NotFound();
            }
            else return Ok(result);
        }

        //[HttpPost("/auto-categorize")]
        //public async Task<TransactionDto> AutoCategorize() {
        //    return null;
        //}
    }
}