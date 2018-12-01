using Sample.Helper.ExceptionLog;
using Sample.Interface.IRepo;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository
{
    public class AccountRepoAsync : IAccountRepoAsync
    {
        SampleAccountContext context;
        private DbContextTransaction dbContextTransaction;
        public AccountRepoAsync(SampleAccountContext context)
        {
            this.context = context;
        }
        public void BeginTransaction()
        {
            dbContextTransaction = context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Commit();
            }
        }
        public void RollbackTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Rollback();
            }
        }
        public void DisposeTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Dispose();
            }
        }
        public async Task<List<TransactionDetail>> GetTransactionsByAccountAsync(long accountNumber)
        {
            try
            {
                var results = await context.TransactionDetails.AsNoTracking().Where(x => x.AccountNumber == accountNumber).ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }
        }
        public async Task<Account> GetAccountByAccountNumberAsync(long accountNumber)
        {
            try
            {
                var result = await context.Accounts.AsNoTracking().Where(x => x.AccountNumber == accountNumber).FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }
        }
        public async Task<TransactionDetail> GetLastTransactionByAccountNumberAsync(long accountNumber)
        {
            try
            {
                var result = await context.TransactionDetails.AsNoTracking().Where(x => x.AccountNumber == accountNumber).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }
        }
        public async Task<int> InsertUpdateTransactionAsync(TransactionDetail transaction)
        {
            try
            {
                context.TransactionDetails.Add(transaction);
                var x = await context.SaveChangesAsync();
                return x;
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }
        }
    }
}
