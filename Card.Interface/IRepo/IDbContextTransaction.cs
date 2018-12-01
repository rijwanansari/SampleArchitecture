using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.IRepo
{
   public interface IDbContextTransaction
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void DisposeTransaction();

    }
}
