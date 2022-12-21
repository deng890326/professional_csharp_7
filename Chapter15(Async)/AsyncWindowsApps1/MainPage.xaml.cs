using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.UI.Core;
using System.Text;
using Windows.UI.Popups;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AsyncWindowsApps1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OnStartAsync(object sender, RoutedEventArgs e)
        {
            text1.Text = GetThreadAndTask($"start {nameof(OnStartAsync)}");
            await Task.Delay(1000);
            text1.Text += $"\nafter await:\n{GetThreadAndTask($"end {nameof(OnStartAsync)}")}";
        }

        private async void OnStartAsyncConfigAwait(object sender, RoutedEventArgs e)
        {
            text1.Text = GetThreadAndTask($"start {nameof(OnStartAsyncConfigAwait)}");
            string ret = await AsyncFunction().ConfigureAwait(continueOnCapturedContext: true);
            text1.Text += $"\nafter await:\n{GetThreadAndTask($"end {nameof(OnStartAsyncConfigAwait)}")}" +
                $"\nret={ret}";
        }

        private async Task<string> AsyncFunction()
        {
            string result = GetThreadAndTask($"start {nameof(AsyncFunction)}");
            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
            result += $"\n{GetThreadAndTask($"end {nameof(AsyncFunction)}")}";

            try
            {
                text1.Text += "this is a call from a wrong thread";
                return "not reached";
            }
            catch (Exception ex) when (ex.HResult == -2147417842)
            {
                Debug.WriteLine(ex);
                return result;
            }
        }

        private async void OnStartAsyncWithThreadSwitch(object sender, RoutedEventArgs e)
        {
            text1.Text = GetThreadAndTask($"start {nameof(OnStartAsyncConfigAwait)}");
            string ret = await AsyncFunctionWithThreadSwitch();
            text1.Text += $"\nafter await:\n{GetThreadAndTask($"end {nameof(OnStartAsyncConfigAwait)}")}" +
                $"\nret={ret}";
        }

        private async Task<string> AsyncFunctionWithThreadSwitch()
        {
            StringBuilder result = new StringBuilder();
            result.Append(GetThreadAndTask($"start {nameof(AsyncFunctionWithThreadSwitch)}"));
            await AppendString(result).ConfigureAwait(continueOnCapturedContext: false);
            result.Append($"\n{GetThreadAndTask($"end {nameof(AsyncFunctionWithThreadSwitch)}")}");

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                text1.Text += $"\n{GetThreadAndTask("async funtion switch back to the UI thread")}";
            });

            return result.ToString();
        }

        private static Task<StringBuilder> AppendString(StringBuilder sb)
        {
            sb.Append($"\n{GetThreadAndTask(nameof(AppendString))}");
            return Task.Run(() =>
            {
                sb.Append($"\n{GetThreadAndTask($"{nameof(AppendString)} start task")}");
                long l = long.MinValue;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    l += i;
                }
                return sb.Append('\n').Append(l)
                .Append($"\n{GetThreadAndTask($"{nameof(AppendString)} end task")}");
            });
        }

        private async void OnIAsyncOperation(object sender, RoutedEventArgs e)
        {
            var dlg = new MessageDialog("Select One, Two, Or Three", "Simple");
            dlg.Commands.Add(new UICommand("One"));
            dlg.Commands.Add(new UICommand("Two"));
            dlg.Commands.Add(new UICommand("Three"));
            var result = await dlg.ShowAsync();
            text1.Text = $"{result.Label} invoked";
        }

        private void OnStartDeadLock(object sender, RoutedEventArgs e)
        {
            Task<string> task = DelayAsync();
            //Task<StringBuilder> task = AppendString(new StringBuilder());
            task.Wait();
            text1.Text = task.Result.ToString();
        }

        private async Task<string> DelayAsync() =>
            //ConfigureAwait(true) 改为 false 可以解开死锁
            //原理是：continueOnCapturedContext指的是使用调用Task的线程（也就是UI线程）去执行
            //等待任务后续的代码。如果这个值传true，这个方法中await之后的代码会运行在UI线程，
            //OnStartDeadLock中的Wait()调用又会阻塞UI线程，直到DelayAsync的整个任务返回。
            (await AppendString(new StringBuilder()).ConfigureAwait(true)).ToString();

        public static string GetThreadAndTask(string info)
        {
            string taskInfo = $"task(id = {Task.CurrentId?.ToString() ?? "null"})";
            return $"{info} in thread(id={Thread.CurrentThread.ManagedThreadId} and {taskInfo}";
        }


    }
}
