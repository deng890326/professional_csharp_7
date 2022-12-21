using Framewok.Services;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Text;

namespace Framewok.ViewModels
{
    public abstract class EditableItemViewModel<TItem> : ItemViewModel<TItem>, IEditableObject
        where TItem : BindableBase, ICloneable, ICopyOperation<TItem>
    {
        public EditableItemViewModel(IItemsService<TItem> itemsService, TItem item)
            : base(itemsService, item)
        {
            SetupShowingItem();
            BeginEditCmd = new RelayCommand(BeginEdit, () => EditingItem == null);
            CancelEditCmd = new RelayCommand(CancelEdit, () => EditingItem != null);
            EndEditCmd = new RelayCommand(EndEdit, () => EditingItem != null);
            PropertyChanged += (_, ev) =>
            {
                if (ev.PropertyName == nameof(EditingItem))
                {
                    BeginEditCmd.RaiseCanExecuteChanged();
                    CancelEditCmd.RaiseCanExecuteChanged();
                    EndEditCmd.RaiseCanExecuteChanged();
                    RaisePropertyChanged(nameof(IsEditMode));
                    RaisePropertyChanged(nameof(IsReadMode));
                }
            };
        }

        public RelayCommand BeginEditCmd { get; }
        public RelayCommand CancelEditCmd { get; }
        public RelayCommand EndEditCmd { get; }

    public bool IsEditMode => EditingItem != null;
        public bool IsReadMode => EditingItem == null;

        public TItem ShowingItem => EditingItem ?? Item;
        private void SetupShowingItem()
        {
            void showingItemChangedHandler(object? _, PropertyChangedEventArgs ev)
            {
                if (ev.PropertyName == nameof(EditingItem) ||
                    ev.PropertyName == nameof(Item))
                {
                    RaisePropertyChanged(nameof(ShowingItem));
                }
            }
            PropertyChanged += showingItemChangedHandler;
        }

        public event EventHandler<EditEventArgs>? EditEvent;

        public void BeginEdit()
        {
            EditingItem = (TItem)Item.Clone();
            EditEvent?.Invoke(this, EditEventArgs.Begin);
        }

        public void CancelEdit()
        {
            EditingItem = null;
            EditEvent?.Invoke(this, EditEventArgs.Cacel);
        }

        public async void EndEdit()
        {
            try
            {
                using var progress = StartInProgress();
                EditingItem!.CopyTo(Item);
                var item = await ItemsService.AddOrUpdateAsync(Item);
                bool isSuccess = item != null;
                StringBuilder message = new StringBuilder();
                if (item != null)
                {
                    message.AppendLine("save success.");
                    Item = item;
                }
                else
                {
                    message.AppendLine("save error.");
                }
                EditEvent?.Invoke(this, EditEventArgs.End(isSuccess, message.ToString()));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "error {Message} in {EndEdit}", ex.Message, nameof(EndEdit));
                EditEvent?.Invoke(this, EditEventArgs.End(false, ex.Message));
            }
        }

        public enum EditState
        {
            Begin, Cancel, End,
        }

        public class EditEventArgs : EventArgs
        {
            public readonly static EditEventArgs Begin =
                new EditEventArgs(EditState.Begin);
            public readonly static EditEventArgs Cacel =
                new EditEventArgs(EditState.Cancel);
            public static EditEventArgs End(bool isSuccess, string message) =>
                new EditEventArgs(EditState.End, isSuccess, message);

            public EditEventArgs(EditState editState,
                bool isSuccess = true, string message = "")
            {
                EditState = editState;
                IsSuccess = isSuccess;
                Message = message;
            }
                
            public EditState EditState { get; }
            public bool IsSuccess { get; }
            public string Message { get; }
        }

        private TItem? _editingItem;
        private TItem? EditingItem
        {
            get => _editingItem;
            set => Set(ref _editingItem, value);
        }

        protected abstract ILogger Logger { get; }
    }
}
