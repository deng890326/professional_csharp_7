namespace ErrorHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DontHandle();
            //HandleOneError();
            //StartTwoTasks();
            StartTwoTasksParallel();
            //ShowAggregatedExceptionsAsync();
            Console.ReadLine();
        }

        static Task ThrowAfter(int ms, string message) => Task.Run(() =>
        {
            Console.WriteLine($"{nameof(ThrowAfter)}: {message}");
            Thread.Sleep(ms);
            throw new Exception(message);
        });

        static void DontHandle()
        {
            try
            {
                ThrowAfter(1000, "DontHandle");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static async void HandleOneError()
        {
            try
            {
                await ThrowAfter(1000, "HandleOneError");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static async void StartTwoTasks()
        {
            try
            {
                await ThrowAfter(2000, "StartTwoTasks, first");
                await ThrowAfter(1000, "StartTwoTasks, second");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static async void StartTwoTasksParallel()
        {
            DateTime start = DateTime.Now;
            try
            {
                Task t1 = ThrowAfter(2000, $"{nameof(StartTwoTasksParallel)}, first");
                Task t2 = ThrowAfter(1000, $"{nameof(StartTwoTasksParallel)}, second");
                await Task.WhenAll(Task.WhenAll(t1, t2));
            }
            catch (Exception ex)
            {
                DateTime end = DateTime.Now;
                Console.WriteLine($"Exception occurred after {end - start:c}");
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine();
            }

            start = DateTime.Now;
            try
            {
                Task t1 = ThrowAfter(2000, $"{nameof(StartTwoTasksParallel)}, first");
                Task t2 = ThrowAfter(1000, $"{nameof(StartTwoTasksParallel)}, second");
                await Task.WhenAll(Task.WhenAll(t2, t1));
            }
            catch (Exception ex)
            {
                DateTime end = DateTime.Now;
                Console.WriteLine($"Exception occurred after {end - start:c}");
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        static async void ShowAggregatedExceptionsAsync()
        {
            Task taskResult = null;
            DateTime start = DateTime.Now;
            try
            {
                Task t1 = ThrowAfter(2000, $"{nameof(ShowAggregatedExceptionsAsync)}, first");
                Task t2 = ThrowAfter(1000, $"{nameof(ShowAggregatedExceptionsAsync)}, second");
                await (taskResult = Task.WhenAll(Task.WhenAll(t1, t2)));
            }
            catch (Exception ex)
            {
                DateTime end  = DateTime.Now;
                Console.WriteLine($"Exception occurred after {end - start:c}");
                Console.WriteLine(ex);
                foreach (var ex1 in taskResult?.Exception?.InnerExceptions
                                    ?? Enumerable.Empty<Exception>())
                {
                    Console.WriteLine($"inner Exception: {ex1.Message}");
                }
            }
            finally
            {
                Console.WriteLine();
            }

            start = DateTime.Now;
            try
            {
                Task t1 = ThrowAfter(2000, $"{nameof(ShowAggregatedExceptionsAsync)}, first");
                Task t2 = ThrowAfter(1000, $"{nameof(ShowAggregatedExceptionsAsync)}, second");
                await (taskResult = Task.WhenAll(Task.WhenAll(t2, t1)));
            }
            catch (Exception ex)
            {
                DateTime end = DateTime.Now;
                Console.WriteLine($"Exception occurred after {end - start:c}");
                Console.WriteLine(ex);
                foreach (var ex1 in taskResult?.Exception?.InnerExceptions
                    ?? Enumerable.Empty<Exception>())
                {
                    Console.WriteLine($"inner Exception: {ex1.Message}");
                }
            }
            finally
            {
                Console.WriteLine();
            }
        }
    }
}