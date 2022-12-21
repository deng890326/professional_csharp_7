using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SystemTransactionSample
{
    internal class Utilities
    {
        public static bool AbortTx()
        {
            Console.WriteLine("Abort the transaction (y/n)?");
            return Console.ReadLine()?.ToLower() == "y";
        }

        public static void DisplayTxInfo(string title, TransactionInformation? info)
        {
            Console.WriteLine(title);
            Console.WriteLine($"CreationTime: {info?.CreationTime}");
            Console.WriteLine($"Status: {info?.Status}");
            Console.WriteLine($"LocalIdentifier: {info?.LocalIdentifier}");
            Console.WriteLine($"DistributedIdentifier: {info?.DistributedIdentifier}");
        }
    }
}
