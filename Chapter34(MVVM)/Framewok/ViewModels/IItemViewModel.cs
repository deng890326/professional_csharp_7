namespace Framewok.ViewModels
{
    public interface IItemViewModel<out TItem>
        where TItem : BindableBase
    {
        TItem Item { get; }
    }
}
