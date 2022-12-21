namespace PointerPlayground2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CurrencyStruct currency1, currency2;

            unsafe
            {
                Console.WriteLine($"sizeof(CurrencyStruct)={sizeof(CurrencyStruct)}");
                CurrencyStruct* pCurrency = &currency1;
                ulong* pD = &currency1.Dollars;
                byte* pC = &currency1.Cents;
                Console.WriteLine($"&currency1={(ulong)&currency1:X}");
                Console.WriteLine($"&currency2={(ulong)&currency2:X}");
                Console.WriteLine($"&pCurrency={(ulong)&pCurrency:X}");
                Console.WriteLine($"&pD={(ulong)&pD:X}");
                Console.WriteLine($"&pC={(ulong)&pC:X}");
                Console.WriteLine();

                pCurrency->Dollars = 999;
                *pC = 5;
                Console.WriteLine($"currency1={currency1}");
                Console.WriteLine($"currency2={currency2}");
                Console.WriteLine();

                --pCurrency;
                Console.WriteLine($"*pCurrency={*pCurrency}");
                Console.WriteLine();

                *(byte*)(((CurrencyStruct*)pC) - 1) = 99;
                Console.WriteLine($"currency2={currency2}");
                Console.WriteLine();
            }

            Console.WriteLine("Now with classes:");
            CurrencyClass? currency3 = new CurrencyClass();
            unsafe
            {
                fixed (ulong* pD = &currency3.Dollars)
                fixed (byte* pC = &currency3.Cents)
                {
                    Console.WriteLine($"&pD={(ulong)&pD:X}");
                    Console.WriteLine($"&pC={(ulong)&pC:X}");
                    Console.WriteLine($"currency3={currency3}");
                    Console.WriteLine();

                    *pC = 99;
                    Console.WriteLine($"currency3={currency3}");

                    try
                    {
                        *(pD - 1) = uint.MaxValue;
                        Console.WriteLine($"currency3={currency3}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    try
                    {
                        currency3 = null;
                        Console.WriteLine($"currency3={currency3}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }
            }

        }
    }

    struct CurrencyStruct
    {
        public ulong Dollars;
        public byte Cents;
        public override string ToString() => $"${Dollars}.{Cents:00}";
    }

    class CurrencyClass
    {
        public ulong Dollars;
        public byte Cents;
        public override string ToString() => $"${Dollars}.{Cents:00}";
    }
}