using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel.Design;
using System.Windows.Input;

namespace DISampleLib
{
    public class ShowMessageViewModel
    {
        public ShowMessageViewModel(IMessageService messageService)
        {
            _messageService = messageService;
            ShowMessageCommand = new RelayCommand(showMessage);
        }

        public ICommand ShowMessageCommand { get; }

        private async void showMessage() => await _messageService.ShowMessageAsync("A message from the view-model");

        private readonly IMessageService _messageService;
    }
}
