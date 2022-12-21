namespace MyStructLib
{
    public struct MyStruct
    {
        public int n;
        //string s;
        long l = 1;

        public MyStruct()
        {
            n = 0;
        }

        public override string ToString() => $"n={n},l={l}";
    }
}