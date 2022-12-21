namespace TypeConvertorTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var balance = new Currency
            {
                Dollars = 50,
                Cents = 35
            };
            Console.WriteLine(balance);
            Console.WriteLine($"balance is {balance}");
            float balance2 = balance;
            Console.WriteLine($"After converting to float, = {balance2}");
            balance = (Currency)balance2;
            Console.WriteLine($"After converting back to Currency, = {balance}");

            try
            {
                float invalidCurrency = float.MaxValue;
                Console.WriteLine($"Now attempt to convert out of range value of {invalidCurrency} to Currency:");
                checked
                {
                    balance = (Currency)invalidCurrency;
                }
                Console.WriteLine($"balance is {balance}");
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

    struct Currency
    {
        public uint Dollars;
        public byte Cents;

        public override string ToString() => $"${Dollars}.{Cents}";

        public static implicit operator float(in Currency currency)
        {
            return currency.Dollars + currency.Cents / 100f;
        }

        public static  explicit operator Currency(in float value)
        {
            checked
            {
                return new Currency
                {
                    Dollars = (uint)value,
                    //Cents = (byte)(value % 1 * 100)
                    Cents = Convert.ToByte(value % 1 * 100)
                };
            }
        }
    }
}