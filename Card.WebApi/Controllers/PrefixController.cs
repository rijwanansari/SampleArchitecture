using Sample.Interface.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sample.WebApi.Controllers
{
    [RoutePrefix("api/Prefix")]
    public class PrefixController : ApiController
    {
        private IAccountService accountService;
        public PrefixController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpGet]
        [Route("GetAllCurrency")]
        public async Task<IHttpActionResult> GetAllCurrency()
        {
            return Ok(await accountService.GetAllCurrencyAsync());
        }
    }
}
