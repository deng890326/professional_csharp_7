using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Threading;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AsyncWindowsApps
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
            text1.Text += $"\nafter await: {GetThreadAndTask($"end {nameof(OnStartAsync)}")}";
        }

        public static string GetThreadAndTask(string info)
        {
            string taskInfo = $"task(id = {Task.CurrentId?.ToString() ?? "null"})";
            return $"{info} in thread(id={Thread.CurrentThread.ManagedThreadId} and {taskInfo}";
        }
    }
}
