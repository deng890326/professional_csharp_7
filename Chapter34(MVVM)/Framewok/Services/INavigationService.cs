using System.Threading.Tasks;

namespace Framewok.Services
{
    public interface INavigationService
    {
        bool UseNavigation();

        Task NavigateToAsync(string page);

        Task NavigateBackAsnc();

        string CurrentPage { get; }
    }
}
