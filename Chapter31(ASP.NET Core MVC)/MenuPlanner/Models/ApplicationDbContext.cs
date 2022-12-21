using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MenuPlanner.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
    }
}
