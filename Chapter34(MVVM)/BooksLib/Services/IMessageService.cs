using System.Threading.Tasks;

namespace BooksLib.Services
{
    public interface IMessageService
    {
        Task ShowMessageAsync(string title, string message);
    }
}
