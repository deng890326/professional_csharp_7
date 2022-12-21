using System.Globalization;

namespace WebSampleApp
{
    internal class SessionSample
    {
        private const string SessionVisits = nameof(SessionVisits);
        private const string SessionTimeCreated = nameof(SessionTimeCreated);

        internal static Task SessionAsync(HttpContext context)
        {
            int visits = increaseVisits(context.Session);
            string timeCreated = context.Session.GetString(SessionTimeCreated)
                ?? setTimeCreated(context.Session);
            return context.Response.WriteAsync($"You have visited {visits} time(s).\n" +
                $"first visit time: {timeCreated}\n" +
                $"now: {DateTime.Now:G}");

            static int increaseVisits(ISession session)
            {
                int visits = session.GetInt32(SessionVisits) ?? 0;
                visits += 1;
                session.SetInt32(SessionVisits, visits);
                return visits;
            }

            static string setTimeCreated(ISession session)
            {
                string ret = DateTime.Now.ToString("G", CultureInfo.InvariantCulture);
                session.SetString(SessionTimeCreated, ret);
                return ret;
            }
        }

 
    }
}