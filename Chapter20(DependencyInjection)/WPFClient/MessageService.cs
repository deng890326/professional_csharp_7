using DISampleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClient
{
    internal class MessageService : IMessageService
    {
        public Task ShowMessageAsync(string message)
        {
            return Task.Run(() => MessageBox.Show(message));
        }
    }
}
