using Sample.Helper;
using Sample.Interface.IRepo;
using Sample.Interface.IServices;
using Sample.Model;
using Sample.Utility.Enum;
using Sample.Utility.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Sample.Helper.Common;
using Sample.Core.Common;
using System.Web;
using System.Net.Http;
using System.Net;

namespace Sample.Core
{
    public class AccountService : IAccountService
    {
        private IAccountRepoAsync accountRepo;
        private IRepository<Account> accountGenericRepo;
        private IRepository<Currency> currencyGenericRepo;
        private IRepository<TransactionLog> transactionLogGenericRepo;
        private ICurrencyRepo currencyRepo;
        public AccountService(IAccountRepoAsync accountRepo, IRepository<Account> accountGenericRepo, IRepository<Currency> currencyGenericRepo, ICurrencyRepo currencyRepo, IRepository<TransactionLog> transactionLogGenericRepo)
        {
            this.accountRepo = accountRepo;
            this.accountGenericRepo = accountGenericRepo;
            this.currencyGenericRepo = currencyGenericRepo;
            this.currencyRepo = currencyRepo;
            this.transactionLogGenericRepo = transactionLogGenericRepo;
        }
        public async Task<ResponseModelAccount> BalanceTransaction(long accountNumber)
        {
            try
            {
                List<TransactionDetail> transactions = await accountRepo.GetTransactionsByAccountAsync(accountNumber);
                Account currentAccount = await accountRepo.GetAccountByAccountNumberAsync(accountNumber);
                Currency baseCurrency = currentAccount.Currency;
                UserModel currentUser = GetDummyUser();
                decimal balance = 0;
                if (transactions.Count > 0)
                {
                    balance = GetBalanceWithTransactions(ref transactions);
                    var lastTransaction = transactions.OrderByDescending(x => x.Id).FirstOrDefault();
                    if (balance == lastTransaction.CurrentBalance)
                    {
                        await LogBalanceCheck(currentAccount, lastTransaction, EnumTransactionType.Balance, currentUser, EnumTransactionStatus.Success.ToString());
                        return HelperClass.Response(true
                                   , GlobalDecleration._successAction
                                   , accountNumber
                                   , balance
                                   , baseCurrency.Code
                               );
                    }                       
                    else
                    {
                        await LogBalanceCheck(currentAccount, lastTransaction, EnumTransactionType.Balance, currentUser, EnumTransactionStatus.Fail.ToString());
                        return HelperClass.Response(false
                              , GlobalDecleration._internalServerError
                              , accountNumber
                              , 0
                              , null
                          );
                    }
                }
                else
                {
                    return HelperClass.Response(true
                                , GlobalDecleration._successAction
                                , accountNumber
                                , balance
                                , baseCurrency.Code
                            );
                }
            }
            catch (Exception ex)
            {
                Print("BalanceTransaction", ex.Message + " InnerException: " + Convert.ToString(ex.InnerException));
                return HelperClass.Response(false
                               , GlobalDecleration._internalServerError
                               , accountNumber
                               , 0
                               , null
                           );

            }
        }
        public async Task<ResponseModelAccount> DepositTransaction(TransactionInputModel transactionInputModel)
        {
            try
            {
                Account currentAccount = await accountRepo.GetAccountByAccountNumberAsync(Convert.ToInt64(transactionInputModel.accountNumber));
                if (currentAccount != null && transactionInputModel.amount > 0)
                {
                    Currency depositCurrency = await currencyRepo.GetCurrencyByCode(transactionInputModel.currency);
                    decimal baseCurrencyAmount = await ConvertCurrency(transactionInputModel.amount, transactionInputModel.currency, currentAccount.Currency.IsoCode);
                    //accountRepo.BeginTransaction();
                    TransactionDetail LastTransaction = await accountRepo.GetLastTransactionByAccountNumberAsync(Convert.ToInt64(currentAccount.AccountNumber));
                    UserModel currentUser = GetDummyUser();
                    TransactionDetail transaction = new TransactionDetail();
                    transaction.Amount = transactionInputModel.amount;
                    transaction.TransactionTypeId = (int)EnumTransactionType.Deposit;
                    transaction.AccountId = currentAccount.Id;
                    transaction.AccountNumber = currentAccount.AccountNumber;
                    transaction.Amount = transactionInputModel.amount;
                    transaction.AmountInBaseCurrency = Math.Round(baseCurrencyAmount, GlobalDecleration.decimalPoints);
                    if (LastTransaction != null)
                        transaction.CurrentBalance = LastTransaction.CurrentBalance + transaction.AmountInBaseCurrency;
                    else
                        transaction.CurrentBalance = transaction.AmountInBaseCurrency;
                    transaction.Created = DateTime.Now;
                    transaction.Modified = DateTime.Now;
                    transaction.Author = currentUser.id;
                    transaction.Editor = currentUser.id;
                    transaction.CurrencyDepositId = depositCurrency.Id;
                    transaction.Active = true;
                    var result =await accountRepo.InsertUpdateTransactionAsync(transaction);
                    
                    if (result > 0)
                    {
                        await LogTransaction(currentAccount, transaction, depositCurrency, currentUser, EnumTransactionStatus.Success.ToString());
                        return HelperClass.Response(true
                                  , GlobalDecleration._successAction
                                  , transaction.AccountNumber
                                  , transaction.CurrentBalance
                                  , currentAccount.Currency.IsoCode
                              );
                    }
                    else {
                        await LogTransaction(currentAccount, transaction, depositCurrency, currentUser, EnumTransactionStatus.Fail.ToString());
                        return HelperClass.Response(false
                             , GlobalDecleration._internalServerError
                             , 0
                             , 0
                             , null
                         );
                    }
                    
                }
                else
                {
                    return HelperClass.Response(false
                              , GlobalDecleration._internalServerError
                              , 0
                              , 0
                              , null
                          );
                }
            }
            catch (Exception ex)
            {
                Print("DepositTransaction", ex.Message + " InnerException: " + Convert.ToString(ex.InnerException));
                return HelperClass.Response(false
                               , GlobalDecleration._internalServerError
                               , 0
                               , 0
                               , null
                           );
            }
        }
        public async Task<ResponseModelAccount> SaveTransaction(TransactionInputModel transactionInputModel)
        {
            try
            {
                UserModel currentUser = GetDummyUser();
                TransactionDetail transaction = new TransactionDetail();
                transaction.Amount = transactionInputModel.amount;
                transaction.TransactionTypeId = (int)EnumTransactionType.Deposit;
                transaction.AccountId = 3;
                transaction.AccountNumber = Convert.ToInt64(transactionInputModel.accountNumber);
                transaction.Amount = transactionInputModel.amount;
                transaction.AmountInBaseCurrency = transactionInputModel.amount;
                transaction.CurrentBalance = transaction.AmountInBaseCurrency;
                transaction.Created = DateTime.Now;
                transaction.Modified = DateTime.Now;
                transaction.Author = currentUser.id;
                transaction.Editor = currentUser.id;
                transaction.CurrencyDepositId = 2;

                var result =await accountRepo.InsertUpdateTransactionAsync(transaction);
                //accountRepo.CommitTransaction();
                return HelperClass.Response(true
                          , GlobalDecleration._successAction
                          , transaction.AccountNumber
                          , transaction.AmountInBaseCurrency
                          , transactionInputModel.currency
                      );

            }
            catch (Exception ex)
            {
                Print("DepositTransaction", ex.Message + " InnerException: " + Convert.ToString(ex.InnerException));
                return HelperClass.Response(false
                               , GlobalDecleration._internalServerError
                               , 0
                               , 0
                               , null
                           );
            }
        }
        public async Task<ResponseModelAccount> WithDrawTransaction(TransactionInputModel transactionInputModel)
        {
            try
            {
                Account currentAccount = await accountRepo.GetAccountByAccountNumberAsync(Convert.ToInt64(transactionInputModel.accountNumber));
                if (currentAccount != null && transactionInputModel.amount > 0)
                {

                    Currency depositCurrency = await currencyRepo.GetCurrencyByCode(transactionInputModel.currency);
                    decimal baseCurrencyAmount = await ConvertCurrency(transactionInputModel.amount, transactionInputModel.currency, currentAccount.Currency.IsoCode);
                    //accountRepo.BeginTransaction();
                    TransactionDetail LastTransaction = await accountRepo.GetLastTransactionByAccountNumberAsync(Convert.ToInt64(currentAccount.AccountNumber));
                    if (LastTransaction.CurrentBalance >= baseCurrencyAmount)
                    {
                        UserModel currentUser = GetDummyUser();
                        TransactionDetail transaction = new TransactionDetail();
                        transaction.Amount = transactionInputModel.amount;
                        transaction.TransactionTypeId = (int)EnumTransactionType.Withdraw;
                        transaction.AccountId = currentAccount.Id;
                        transaction.AccountNumber = currentAccount.AccountNumber;
                        transaction.Amount = transactionInputModel.amount;
                        transaction.AmountInBaseCurrency = Math.Round(baseCurrencyAmount, GlobalDecleration.decimalPoints);
                        transaction.CurrentBalance = LastTransaction.CurrentBalance - baseCurrencyAmount;
                        transaction.Created = DateTime.Now;
                        transaction.Modified = DateTime.Now;
                        transaction.Author = currentUser.id;
                        transaction.Editor = currentUser.id;
                        transaction.CurrencyDepositId = depositCurrency.Id;
                        transaction.Active = true;
                        var result =await accountRepo.InsertUpdateTransactionAsync(transaction);
                        if (result > 0)
                        {
                            await LogTransaction(currentAccount, transaction, depositCurrency, currentUser, EnumTransactionStatus.Success.ToString());
                            return HelperClass.Response(true
                                      , GlobalDecleration._successAction
                                      , transaction.AccountNumber
                                      , transaction.CurrentBalance
                                      , currentAccount.Currency.IsoCode
                                  );
                        }
                        else
                        {
                            await LogTransaction(currentAccount, transaction, depositCurrency, currentUser, EnumTransactionStatus.Fail.ToString());
                            return HelperClass.Response(false
                                 , GlobalDecleration._internalServerError
                                 , 0
                                 , 0
                                 , null
                             );
                        }

                    }
                    else
                    {
                        return HelperClass.Response(true
                                 , GlobalDecleration._insufficientBalance
                                 , currentAccount.AccountNumber
                                 , LastTransaction.AmountInBaseCurrency
                                 , currentAccount.Currency.IsoCode
                             );
                    }


                }
                else
                {
                    return HelperClass.Response(false
                              , GlobalDecleration._internalServerError
                              , 0
                              , 0
                              , null
                          );
                }
            }
            catch (Exception ex)
            {
                Print("WithDrawTransaction", ex.Message + " InnerException: " + Convert.ToString(ex.InnerException));
                return HelperClass.Response(false
                               , GlobalDecleration._internalServerError
                               , 0
                               , 0
                               , null
                           );
            }
        }

        //Prefix for POC
        public async Task<ResponseModel> GetAllCurrencyAsync()
        {
            try
            {
                var results = await currencyGenericRepo.Table.ToListAsync();
                return HelperClass.Response(true
                                , GlobalDecleration._successAction
                                , results
                            );
            }
            catch (Exception ex)
            {
                Print("GetAllCurrencyAsync", ex.Message);
                return HelperClass.Response(false
                               , GlobalDecleration._internalServerError
                               , null
                           );
            }
        }

        #region Methods
        private decimal GetBalanceWithTransactions(ref List<TransactionDetail> transactions)
        {
            decimal balance = 0;
            try
            {

                var depositTransactions = transactions.Where(x => x.TransactionTypeId == (int)EnumTransactionType.Deposit && x.Active == true).ToList();
                var withDrawTransactions = transactions.Where(x => x.TransactionTypeId == (int)EnumTransactionType.Withdraw && x.Active == true).ToList();
                decimal sumOfDeposits = depositTransactions.Sum(x => x.AmountInBaseCurrency);
                decimal sumOfWithdraws = withDrawTransactions.Sum(x => x.AmountInBaseCurrency);
                balance = sumOfDeposits - sumOfWithdraws;
                return balance;
            }
            catch (Exception ex)
            {
                Print("BalanceTransaction", ex.Message + " InnerException: " + Convert.ToString(ex.InnerException));
                return balance;
            }
        }
        private async Task<decimal> ConvertCurrency(decimal amount, string sourceCode, string destinationCode)
        {
            decimal returnAmount = 0;
            try
            {
                CurrencyRate sourceCurrencyRate = await currencyRepo.GetCurrencyRateByCode(sourceCode);
                CurrencyRate destinationCurrencyRate = await currencyRepo.GetCurrencyRateByCode(destinationCode);
                decimal baseCurrencyAmount = amount / sourceCurrencyRate.Rate;
                decimal destinationCurrencyAmount = baseCurrencyAmount * destinationCurrencyRate.Rate;
                returnAmount = destinationCurrencyAmount;
            }
            catch (Exception ex)
            {
                Print("BalanceTransaction", ex.Message + " InnerException: " + Convert.ToString(ex.InnerException));
                returnAmount = 0;
            }
            return returnAmount;
        }
        private async Task<bool> LogTransaction(Account account, TransactionDetail transactionDetail, Currency currenctCurrency, UserModel currentUser, string result)
        {
            bool retVal = false;
            try
            {
                var hostname = HttpContext.Current.Request.UserHostAddress;
                IPAddress ipAddress = IPAddress.Parse(hostname);
                IPHostEntry ipHostEntry = Dns.GetHostEntry(ipAddress);
                var currencyRate= await currencyRepo.GetCurrencyRateByCode(currenctCurrency.IsoCode);
                TransactionLog transactionLog = new TransactionLog()
                {
                    HostIP = ipAddress.ToString(),
                    TransactionId = transactionDetail.Id,
                    AccountId= account.Id,
                    AccountNumber= account.AccountNumber,
                    TransactionTypeId= transactionDetail.TransactionTypeId,
                    CurrencyRateWithBase = currencyRate.Rate,
                    TransactionCurrency= currenctCurrency.IsoCode,
                    BaseCurrency = account.Currency.IsoCode,
                    TransactionDate = DateTime.Now,
                    BalanceAmountBaseCurrency = transactionDetail.CurrentBalance,
                    Status = result,
                    Active = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Author = currentUser.id,
                    Editor = currentUser.id
                };
                var insertResult = await transactionLogGenericRepo.Insert(transactionLog);
                if (insertResult > 0)
                    retVal = true;

            }
            catch (Exception ex)
            {
                Print("", ex.Message);
                retVal = false;
            }
            return retVal;
        }
        private async Task<bool> LogBalanceCheck(Account account, TransactionDetail transactionDetail, EnumTransactionType transactionType, UserModel currentUser, string result)
        {
            bool retVal = false;
            try
            {
                var hostname = HttpContext.Current.Request.UserHostAddress;
                IPAddress ipAddress = IPAddress.Parse(hostname);
                IPHostEntry ipHostEntry = Dns.GetHostEntry(ipAddress);
                TransactionLog transactionLog = new TransactionLog()
                {
                    HostIP = ipAddress.ToString(),
                    AccountId = account.Id,
                    AccountNumber = account.AccountNumber,
                    TransactionTypeId = (int)transactionType,
                    BaseCurrency = account.Currency.IsoCode,
                    TransactionDate = DateTime.Now,
                    BalanceAmountBaseCurrency = transactionDetail.CurrentBalance,
                    Status = result,
                    Active = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Author = currentUser.id,
                    Editor = currentUser.id
                };
                var insertResult = await transactionLogGenericRepo.Insert(transactionLog);
                if (insertResult > 0)
                    retVal = true;

            }
            catch (Exception ex)
            {
                Print("", ex.Message);
                retVal = false;
            }
            return retVal;
        }
        private UserModel GetDummyUser()
        {
            UserModel user = new UserModel
            {
                id = 1,
                accountName = "sample\\rijwan",
                email = "rijwan@sample.com",
                displayName = "Rijwan Ansari"
            };
            return user;
        }

        public string GetUserIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var ctx = request.Properties["MS_HttpContext"] as HttpContextBase;
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            return null;
        }
        #endregion

        #region Error
        private static void Print(string method
             , string msg)
        {
            ErrorLogs.PrintError("AccountService : IAccountService"
                , method
                , msg);
        }

        #endregion Error
    }
}
