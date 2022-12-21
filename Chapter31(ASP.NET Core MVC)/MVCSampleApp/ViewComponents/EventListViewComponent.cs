using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using static MVCSampleApp.ViewComponents.ViewDataKeys;

namespace MVCSampleApp.ViewComponents
{
    public class EventListViewComponent : ViewComponent
    {
        public EventListViewComponent(EventsAndMenusContext context) =>
            _context = context;

        public Task<IViewComponentResult> InvokeAsync(DateTime from, DateTime to)
        {
            return Task.Run(() => {
                ViewData[EventListDefaultHeader] =
                    "Formula 1 Calendar";
                return (IViewComponentResult)
                    View(model: EventsByDateRange(from, to));
            });
        }

        private IEnumerable<Event> EventsByDateRange(DateTime fr, DateTime to)
        {
            return (from e in _context.Events
                   where e.Day >= fr && e.Day <= to
                   select e)
                   .ToArray();
        }

        private EventsAndMenusContext _context;
    }
}
