using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Framewok
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged(
            [CallerMemberName] string propertyName = "")
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }

        protected void Set<T>(ref T propertyField, T value,
            [CallerMemberName] string propertyName = "")
        {
            var comparer = EqualityComparer<T>.Default;
            if (comparer.Equals(propertyField, value))
                return;
            propertyField = value;
            RaisePropertyChanged(propertyName);
        }
    }
}