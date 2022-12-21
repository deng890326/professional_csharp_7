using System;

namespace Framewok.ViewModels
{
    public sealed class StateSetter : IDisposable
    {
        private Action _end;

        public StateSetter(Action start, Action end)
        {
            start();
            _end = end;
        }

        public void Dispose()
        {
            _end();
        }
    }
}
