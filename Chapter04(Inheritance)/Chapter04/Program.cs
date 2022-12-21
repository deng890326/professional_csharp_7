namespace Chapter04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreditAccount creditAccount = new CreditAccount();
            creditAccount.PayIn(100);
            creditAccount.WithDraw(50);
            creditAccount.WithDraw(new decimal(50.1));
            Console.WriteLine(creditAccount);
            Console.WriteLine();
            
            IBankAccount bankAccount = new SaverAccount();
            bankAccount.PayIn(100);
            bankAccount.WithDraw(50);
            bankAccount.WithDraw(new decimal(50.1));
            Console.WriteLine(bankAccount);
            Console.WriteLine();

            bankAccount = new SaverAccount2();
            bankAccount.PayIn(100);
            bankAccount.WithDraw(50);
            bankAccount.WithDraw(new decimal(50.1));
            Console.WriteLine(bankAccount);
            Console.WriteLine();
        }
    }

    interface IBankAccount
    {
        void PayIn(decimal amount);
        bool WithDraw(decimal amount);
        decimal Balance { get; }
    }

    #region virtual implement interface
    class CreditAccount : IBankAccount
    {

        decimal _balance;

        public void PayIn(decimal amount)
        {
            _balance += amount;
            Console.WriteLine($"Account PayIn({amount}), Balance is {Balance}");
        }

        public virtual bool WithDraw(decimal amount)
        {
            _balance -= amount;
            Console.WriteLine($"Account WithDraw({amount}): Success, Balance is {Balance}");
            return true;
        }

        public decimal Balance { get => _balance; }

        public override string ToString()
        {
            return ($"Account Balance: {Balance,6:C}");
        }
    }

    class SaverAccount : CreditAccount
    {
        public override bool WithDraw(decimal amount)
        {
            if (Balance < amount)
            {
                Console.WriteLine($"Account WithDraw({amount}): Fail(Not Enough Money), Balance is {Balance}.");
                return false;
            }
            return base.WithDraw(amount);
        }
    }
    #endregion

    #region abstract implement interface
    abstract class AbstractAccount : IBankAccount
    {
        decimal _balance;

        public void PayIn(decimal amount)
        {
            _balance += amount;
            Console.WriteLine($"Account PayIn({amount}), Balance is {Balance}");
        }


        public abstract bool WithDraw(decimal amount);

        public decimal Balance { get => _balance; protected set { _balance = value; } }

        public override string ToString()
        {
            return ($"Account Balance: {Balance,6:C}");
        }
    }

    class SaverAccount2 : AbstractAccount
    {
        public override bool WithDraw(decimal amount)
        {
            if (Balance < amount)
            {
                Console.WriteLine($"Account WithDraw({amount}): Fail(Not Enough Money), Balance is {Balance}.");
                return false;
            }

            Balance = Balance - amount;
            Console.WriteLine($"Account WithDraw({amount}): Success, Balance is {Balance}");
            return true;
        }
    }
    #endregion
}