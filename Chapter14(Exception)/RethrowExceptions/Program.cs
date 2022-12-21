namespace RethrowExceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action[] methods =
            {
                HandleAndThrowAgain,
                HandlerAndThrowWithInnerException,
                HandleAndRethrow,
                HandleWithFilter
            };

            foreach (Action action in methods)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{nameof(Main)}:\nexception is ({ex}).");
                    //Console.WriteLine($"exception is ({ex.Message}).\n{ex.StackTrace}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"inner exception is ({ex.InnerException})");
                    //Console.WriteLine($"exception is ({ex.InnerException.Message}).\n" +
                    //    $"{ex.InnerException.StackTrace}");
                }
                finally
                {
                    Console.WriteLine();
                }
            }
        }

        static void ThrowAnException(string message) =>
            throw new MyCustomException(message);

        static void HandleAndThrowAgain()
        {
            try
            {
                ThrowAnException("test 1");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(HandleAndThrowAgain)}:\n" +
                    $"Log exception ({ex}).");
                throw ex;
            }
        }

        static void HandlerAndThrowWithInnerException()
        {
            try
            {
                ThrowAnException("test 2");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(HandlerAndThrowWithInnerException)}:\n" +
                    $"Log exception ({ex}).");
                throw new AnotherCustomException("inner exception", ex);
            }
        }

        static void HandleAndRethrow()
        {
            try
            {
                ThrowAnException("test 3");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(HandleAndRethrow)}:\n" +
                    $"Log exception ({ex})");
                throw;
            }
        }

        static void HandleWithFilter()
        {
            try
            {
                ThrowAnException("test 4");
            }
            catch (Exception ex) when (Filter(ex))
            {
                Console.WriteLine("block never invoked");
            }

            static bool Filter(Exception ex)
            {
                Console.WriteLine($"just log {ex}");
                return false;
            }
        }
    }
}