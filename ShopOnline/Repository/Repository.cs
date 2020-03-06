using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ShopOnline.Repository
{
    public static class Repository
    {
        public static TransactionScope GetNewReadUncommittedScope()
        {
            return new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
