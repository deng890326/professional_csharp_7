using System.Reflection;

namespace DynamicSamples
{
    internal class Program
    {
        private const string CalculatorTypeName = "CalculatorLib.Calculator";

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Usage: {nameof(DynamicSamples)} {{path_to_dll}}.");
                return;
            }

            UsingReflection(args[0]);
            UsingReflectionWithDynamic(args[0]);
        }

        private static object? GetCalculator(string assemblyPath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                return assembly.CreateInstance(CalculatorTypeName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private static void UsingReflectionWithDynamic(string assemblyPath)
        {
            dynamic? cal = GetCalculator(assemblyPath);
            double x = 3, y = 4;
            double? ret = cal?.Subtract(x, y);
            Console.WriteLine($"the result of {x} and {y} is {ret}");

            try
            {
                ret = cal?.Multiply(x, y);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void UsingReflection(string assemblyPath)
        {
            object? cal = GetCalculator(assemblyPath);
            if (cal == null)
            {
                Console.WriteLine($"{assemblyPath} does not define {CalculatorTypeName}.");
                return;
            }

            const string methodName = "Add";
            MethodInfo? add = cal.GetType().GetMethod(methodName);
            if (add == null)
            {
                Console.WriteLine($"{CalculatorTypeName} does not define {methodName}.");
                return;
            }

            object[] @params = { 3, 4 };
            object? ret = add.Invoke(cal, @params);
            Console.WriteLine($"the result of {@params[0]} and {@params[1]} is {ret}");
        }
    }
}