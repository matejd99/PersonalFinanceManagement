using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Services;

namespace PersonalFinanceManagement.Controllers
{

    [Route("List")]
    [ApiController]
    public class ListTransactionsController
    {
        private ListTransactionsService listTransactionsService;

        public ListTransactionsController(ListTransactionsService listTransactionsService)
        {
            this.listTransactionsService = listTransactionsService;
        }

        [HttpGet("Transactions")]
        public void getTransactions()
        {
            
        
        
        }

            /*
            TransactionsContext transactions = new TransactionsContext();
            transactions.Transactions.Where(transaction => transaction.Id == 0);
             */
        }
 }

