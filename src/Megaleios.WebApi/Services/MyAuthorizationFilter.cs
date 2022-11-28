using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Megaleios.WebApi.Services
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}