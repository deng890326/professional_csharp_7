namespace AsyncOverrideTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var imp = new ImpClass();
            IInterface i = imp;
            i.RunOneTask();
            AbsClass abs = imp;
            abs.DoSomething();

            Console.WriteLine($"{nameof(Main)} completed.");
            Console.ReadLine();
        }
    }

    interface IInterface
    {
        Task RunOneTask();
    }

    abstract class AbsClass : IInterface
    {
        public async Task RunOneTask()
        {
            await Task.Delay(1000);
            Console.WriteLine($"{nameof(RunOneTask)} completed.");
        }

        public virtual void DoSomething() { }
    }

    class ImpClass : AbsClass
    {
        public override async void DoSomething()
        {
            await Task.Delay(2000);
            Console.WriteLine($"{nameof(DoSomething)} completed.");
        }
    }
}