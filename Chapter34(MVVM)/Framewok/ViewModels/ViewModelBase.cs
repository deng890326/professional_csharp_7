using System;
using System.Threading;

namespace Framewok.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        public bool IsInProgress => _inProgressCounter > 0;

        public IDisposable StartInProgress() =>
            new StateSetter(start: increaseInProgressCounter,
                            end: decreaseInProgressCounter);

        private void increaseInProgressCounter()
        {
            Interlocked.Increment(ref _inProgressCounter);
            RaisePropertyChanged(nameof(IsInProgress));
        }

        private void decreaseInProgressCounter()
        {
            Interlocked.Decrement(ref _inProgressCounter);
            RaisePropertyChanged(nameof(IsInProgress));
        }

        private int _inProgressCounter;
    }
}
