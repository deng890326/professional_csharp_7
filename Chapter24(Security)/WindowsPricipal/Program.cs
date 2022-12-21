using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace WindowsPricipal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowIdentity();
        }

        static void ShowIdentity()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                Console.WriteLine("not a Windows Identity.");
                return;
            }
            Console.WriteLine($"IdentityType: {identity}");
            Console.WriteLine($"Name: {identity.Name}");
            Console.WriteLine($"Authenticated: {identity.IsAuthenticated}");
            Console.WriteLine($"Authentication Type: {identity.AuthenticationType}");
            Console.WriteLine($"Anonymous ? {identity.IsAnonymous}");
            Console.WriteLine($"Access Token: {identity.AccessToken.DangerousGetHandle()}");
            Console.WriteLine();

            WindowsPrincipal principal = new WindowsPrincipal(identity);
            Console.WriteLine("principal");
            ShowPrincipal(principal);
            Console.WriteLine();


            Console.WriteLine("identity.Claims");
            ShowClaims(identity.Claims);
            Console.WriteLine();

            Console.WriteLine("identity.DeviceClaims");
            ShowClaims(identity.DeviceClaims);
            Console.WriteLine();

            Console.WriteLine("identity.UserClaims");
            ShowClaims(identity.UserClaims);
            Console.WriteLine();

            Console.WriteLine("principal.Claims");
            ShowClaims(principal.Claims);
            Console.WriteLine();

            Console.WriteLine("principal.DeviceClaims");
            ShowClaims(principal.DeviceClaims);
            Console.WriteLine();

            Console.WriteLine("principal.UserClaims");
            ShowClaims(principal.UserClaims);
            Console.WriteLine();
        }

        private static void ShowPrincipal(WindowsPrincipal principal)
        {
            foreach (WindowsBuiltInRole role in Enum.GetValues(typeof(WindowsBuiltInRole)))
            {
                Console.WriteLine($"is {role,-15} ? {principal.IsInRole(role)}");
            }
            var principalProperties = typeof(WindowsPrincipal).GetProperties();
            foreach (var prop in principalProperties)
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(principal)}");
            }
        }

        static void ShowClaims(IEnumerable<Claim> claims)
        {
            IEnumerable<PropertyInfo> claimProperties = 
                typeof(Claim).GetProperties();
            foreach (Claim claim in claims)
            {
                foreach (PropertyInfo property in claimProperties)
                {
                    Console.WriteLine($"{property.Name}: {property.GetValue(claim)}");
                }
                foreach (var prop in claim.Properties)
                {
                    Console.WriteLine($"Property: {prop.Key} {prop.Value}");
                }
                Console.WriteLine();
            }
        }
    }
}