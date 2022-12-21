internal class Program
{
    private static void Main(string[] args)
    {
        Data data = new Data(100);
        int n = data.Number;
        n++;
        data.Show();
        Console.WriteLine();

        ref int refN = ref data.Number;
        refN++;
        data.Show();
        Console.WriteLine();

        ref readonly int crefN = ref data.Number;
        //crefN = crefN + 1;


        int n2 = data.ReadonlyNumber;
        n2++;
        data.Show();
        Console.WriteLine();

        //ref int refN2 = ref data.ReadonlyNumber;
        //refN2++;
        ref readonly int refN2 = ref data.ReadonlyNumber;
        data.Show();
        Console.WriteLine();

        ref readonly int crefN2 = ref data.ReadonlyNumber;
        //crefN2 = crefN2 + 1;
    }

    public class Data
    {
        public Data(int n)
        {
            number = n;
        }

        public ref int Number => ref number;

        public ref readonly int ReadonlyNumber => ref number;

        public void Show() => Console.WriteLine($"Data: {number}");

        private int number;
    }
}