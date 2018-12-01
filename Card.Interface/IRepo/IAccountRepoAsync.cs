using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.IRepo
{
   public interface IAccountRepoAsync
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void DisposeTransaction();
        Task<List<TransactionDetail>> GetTransactionsByAccountAsync(long accountNumber);
        Task<Account> GetAccountByAccountNumberAsync(long accountNumber);
        Task<TransactionDetail> GetLastTransactionByAccountNumberAsync(long accountNumber);
        Task<int> InsertUpdateTransactionAsync(TransactionDetail transaction);
    }
}
