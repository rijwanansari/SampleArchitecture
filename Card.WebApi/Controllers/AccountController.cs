using Sample.Interface.IServices;
using Sample.Utility.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sample.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost]
        [Route("Deposit")]
        public async Task<IHttpActionResult> Deposit(TransactionInputModel transactionInputModel)
        {
            return Ok(await accountService.DepositTransaction(transactionInputModel));
        }
        //[HttpPost]
        //[Route("Save")]
        //public async Task<IHttpActionResult> Save(TransactionInputModel transactionInputModel)
        //{
        //    return Ok(await accountService.SaveTransaction(transactionInputModel));
        //}
        [HttpPost]
        [Route("Withdraw")]
        public async Task<IHttpActionResult> Withdraw(TransactionInputModel transactionInputModel)
        {
            return Ok(await accountService.WithDrawTransaction(transactionInputModel));
        }
        [HttpGet]
        [Route("Balance")]
        public async Task<IHttpActionResult> GetBalance(long accountNumber)
        {
            return Ok(await accountService.BalanceTransaction(accountNumber));
        }
    }
}
