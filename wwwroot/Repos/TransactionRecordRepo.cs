using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Repos
{
    class TransactionRecordRepo
    {
        PayPalExerciseEntities context = new PayPalExerciseEntities();

        public IEnumerable<TransactionRecord> GetAllTransactionRecords()
        {
            IEnumerable<TransactionRecord> allTransactionRecords = (from c in context.TransactionRecords
                                                                    select c).OrderByDescending(d => d.txTime);
            return allTransactionRecords;
        }
    }
}
