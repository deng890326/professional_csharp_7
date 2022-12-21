namespace PointerPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 10;
            short y = -1;
            byte y2 = 4;
            double z = 5.234;

            unsafe
            {
                int* px = &x;
                short* py = &y;
                double* pz = &z;
                Console.WriteLine($"&x=0x{(ulong)&x:X}, sizeof(x)={sizeof(int)}, x={x}");
                Console.WriteLine($"&y=0x{(ulong)&y:X}, sizeof(y)={sizeof(short)}, y={y}");
                Console.WriteLine($"&y2=0x{(ulong)&y2:X}, sizeof(y2)={sizeof(byte)}, y2={y2}");
                Console.WriteLine($"&z=0x{(ulong)&z:X}, sizeof(z)={sizeof(double)}, z={z}");

                Console.WriteLine($"&px=0x{(ulong)&px:X}, sizeof(px)={sizeof(int*)}, px={(ulong)px}");
                Console.WriteLine($"&py=0x{(ulong)&py:X}, sizeof(py)={sizeof(short*)}, py={(ulong)py}");
                Console.WriteLine($"&pz=0x{(ulong)&pz:X}, sizeof(pz)={sizeof(double*)}, pz={(ulong)pz}");

                *(px - 2) = 2;
                Console.WriteLine(y);
                Console.WriteLine(*(double*)px);
                Console.WriteLine(*(bool*)py);
                Console.WriteLine(*(long*)pz);
                for (int i = 0; i < int.MaxValue; i++)
                {
                    int num = 1 << i;
                    //int num = i;
                    Console.Write($"{num}: ");
                    try
                    {
                        Console.WriteLine(*(px - num));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        break;
                    }
                }
            }
        }
    }
}