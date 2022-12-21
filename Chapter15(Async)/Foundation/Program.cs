using System.Net;
using System.Threading.Tasks;
using ThreadTaskInfo;

namespace Foundation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine(Greeting("World"));
            //Console.WriteLine();

            var task = CallerWithAsync();
            await task;
            Console.WriteLine($"CallerWithAsync, task={task.GetType()}");

            var task1 = CallerWithAsync2();
            var ret = await task1;
            Console.WriteLine($"CallerWithAsync, task={task1.GetType()}, ret={ret}");

            //var task = CallerWithAsyncReturnValue();
            //Console.WriteLine($"{await task}\n.");
            //Console.WriteLine($"CallerWithAsyncReturnValue, task={task.GetType()}");

            //var task = CallerWithAwaiter();
            //await task;
            //Console.WriteLine($"CallerWithAwaiter, task={task.GetType()}");

            //var task = CallerWithAwaiter2();
            //await task;
            //Console.WriteLine($"CallerWithAwaiter2, task={task.GetType()}");

            //var task = CallerWithCotinuationTask();
            //await task;
            //Console.WriteLine($"CallerWithCotinuationTask, task={task.GetType()}");

            //await MultipleAsyncMethods();
            //await MultipleAsyncMethodsWithCombinators();

            //ConvertingAsyncPattern();
            //ConvertingAsyncPattern2();

            Console.ReadLine();
        }

        static async Task CallerWithAsync()
        {
            Tracer.TraceThreadAndTask($"started {nameof(CallerWithAsync)}");
            Console.WriteLine(await GreetingAsync("World With Async"));
            Tracer.TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
            Console.WriteLine();
        }

        static Task<int> CallerWithAsync2() => Task.Run(async () =>
        {
            Tracer.TraceThreadAndTask($"started {nameof(CallerWithAsync2)}");
            Console.WriteLine(await GreetingAsync("World With Async"));
            Tracer.TraceThreadAndTask($"ended {nameof(CallerWithAsync2)}");
            Console.WriteLine();
            return 1;
        });

        static async Task<string> CallerWithAsyncReturnValue()
        {
            Tracer.TraceThreadAndTask($"started {nameof(CallerWithAsync)}");
            string greeting = await GreetingAsync("World With Async");
            Tracer.TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
            return $"{greeting} Return Value";
        }

        static Task<string> CallerWithAwaiter()
        {
            Tracer.TraceThreadAndTask($"started {nameof(CallerWithAwaiter)}");
            Task<string> task = GreetingAsync("World With Awaiter");
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine(awaiter.GetResult());
                Tracer.TraceThreadAndTask($"ended {nameof(CallerWithAwaiter)}");
                Console.WriteLine();
            });
            return task;
        }

        static Task CallerWithAwaiter2()
        {
            Tracer.TraceThreadAndTask($"started {nameof(CallerWithAwaiter2)}");
            return Task.Run(() =>
            {
                Task<string> task = GreetingAsync("World With Awaiter2");
                var awaiter = task.GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    Console.WriteLine(awaiter.GetResult());
                    Tracer.TraceThreadAndTask($"ended {nameof(CallerWithAwaiter2)}");
                    Console.WriteLine();
                });
                task.Wait();
            });
        }

        static Task CallerWithCotinuationTask()
        {
            Tracer.TraceThreadAndTask($"started {nameof(CallerWithCotinuationTask)}");
            Task<string> task = GreetingAsync("World With Cotinuation Task");
            return task.ContinueWith(t =>
            {
                Console.WriteLine(t.Result);
                Tracer.TraceThreadAndTask($"ended {nameof(CallerWithCotinuationTask)}");
                Console.WriteLine();
            });
        }

        static async Task MultipleAsyncMethods()
        {
            string s1 = await GreetingAsync("Yingwei");
            string s2 = await GreetingAsync("LuLu");
            Console.WriteLine($"Finish both methods.\n" +
                $"s1={s1}, s2={s2}");
        }

        static async Task MultipleAsyncMethodsWithCombinators()
        {
            Task<string> t1 = GreetingAsync("Yingwei");
            Task<string> t2 = GreetingAsync("LuLu");
            string[] rets = await Task.WhenAll(t1, t2);
            Console.WriteLine($"Finish both methods.\n" +
                $"{string.Join(",", rets)}\n" +
                $"result1 = {t1.Result}, result2 = {t2.Result}");
        }

        static async void ConvertingAsyncPattern()
        {
            WebRequest httpWebRequest = WebRequest.Create("http://www.baidu.com");
            using (WebResponse response = await Task.Factory.FromAsync(
                httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null))
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                }
            }
        }

        static async void ConvertingAsyncPattern2()
        {
            WebRequest httpWebRequest = WebRequest.Create("http://www.baidu.com");
            using (WebResponse response = await Task.Run(() => httpWebRequest.GetResponse()))
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                }
            }
        }

        static string Greeting(string name)
        {
            Tracer.TraceThreadAndTask($"running {nameof(Greeting)}");
            //Task.Delay(3000).Wait();
            Thread.Sleep(1000);
            return $"Hello, {name}!";
        }

        static Task<string> GreetingAsync(string name) =>
            Task.Run(() =>
            {
                Tracer.TraceThreadAndTask($"running {nameof(GreetingAsync)}");
                return Greeting(name);
            });
    }
}