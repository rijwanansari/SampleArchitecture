using Sample.Helper;
using Sample.Utility.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.IServices
{
   public interface IAccountService
    {
        Task<ResponseModelAccount> BalanceTransaction(long accountNumber);
        Task<ResponseModelAccount> DepositTransaction(TransactionInputModel transactionInputModel);
        Task<ResponseModelAccount> WithDrawTransaction(TransactionInputModel transactionInputModel);
        Task<ResponseModelAccount> SaveTransaction(TransactionInputModel transactionInputModel);
        Task<ResponseModel> GetAllCurrencyAsync();
    }
}
