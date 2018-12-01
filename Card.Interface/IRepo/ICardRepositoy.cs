using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.IRepo
{
  public interface  ICardRepositoy
    {
        IEnumerable<string> ValidateCardWithStoreProcedure(string query, params object[] parameters);
    }
}
