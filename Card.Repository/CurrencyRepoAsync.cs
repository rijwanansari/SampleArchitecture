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
    public class CurrencyRepoAsync : ICurrencyRepo
    {
        SampleAccountContext context;
        public CurrencyRepoAsync(SampleAccountContext context)
        {
            this.context = context;
        }
        public async Task<List<Currency>> GetCurrencies()
        {
            try
            {
                using (SampleAccountContext contect = new SampleAccountContext())
                {
                    var results = await context.Currencies.AsNoTracking().ToListAsync();
                    return results;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }
        }

        public async Task<CurrencyRate> GetCurrencyRateByCode(string code)
        {
            try
            {
                using (SampleAccountContext contect = new SampleAccountContext())
                {
                    Currency currency = await context.Currencies.AsNoTracking().Where(x=>x.IsoCode==code).FirstOrDefaultAsync();
                    CurrencyRate currencyRate = await context.CurrencyRates.AsNoTracking().Where(x => x.CurrencyId == currency.Id).OrderByDescending(o=>o.Id).FirstOrDefaultAsync();
                    return currencyRate;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }
        }
        public async Task<Currency> GetCurrencyByCode(string code)
        {
            try
            {
                using (SampleAccountContext contect = new SampleAccountContext())
                {
                    Currency currency = await context.Currencies.AsNoTracking().Where(x => x.IsoCode == code).FirstOrDefaultAsync();
                    return currency;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }
        }
    }
}
