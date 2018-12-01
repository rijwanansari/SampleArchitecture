using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.IRepo
{
   public interface ICurrencyRepo
    {
        Task<List<Currency>> GetCurrencies();
        Task<CurrencyRate> GetCurrencyRateByCode(string code);
        Task<Currency> GetCurrencyByCode(string code);
    }
}
