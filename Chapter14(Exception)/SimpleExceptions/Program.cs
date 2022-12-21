namespace SimpleExceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int lower = 0, upper = 5;
            while (true)
            {
                int number;
                try
                {
                    Console.Write($"Input a number between {lower} and {upper}, or just hit return to exit>");
                    string? userinput = Console.ReadLine();
                    if (string.IsNullOrEmpty(userinput)) break;
                    number = Convert.ToInt32(userinput);
                    if (number < lower || number > upper)
                        throw new IndexOutOfRangeException($"Number should be between {lower} and {upper}, " +
                            $"you typed in {number}");
                    
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception was throw. Message was: {ex.Message}");
                    continue;
                }
                finally
                {
                    Console.WriteLine("Thank you\n");
                }

                Console.WriteLine($"Your number was {number}");
            }

            try
            {
                Console.WriteLine("Exit.");
            }
            catch (IOException) { }
        }
    }
}