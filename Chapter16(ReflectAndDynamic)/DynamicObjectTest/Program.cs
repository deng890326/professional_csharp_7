using System.Dynamic;

namespace DynamicObjectTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WroxDynamicObject Test:");
            dynamic wroxDynamic = new WroxDynamicObject();
            DoSomethingWith(wroxDynamic);

            Console.WriteLine("ExpandoObject Test:");
            dynamic expando = new ExpandoObject();
            DoSomethingWith(expando);
        }

        private static void DoSomethingWith(dynamic dyn)
        {
            dyn.FirstName = "Bugs";
            dyn.LastName = "Bunny";
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(dyn.FirstName + " " + dyn.LastName);

            dyn.Friends = new List<(string FirstName, string LastName)>();
            dyn.Friends.Add(("Deng", "Yingwei"));
            dyn.Friends.Add(("Chen", "Lu"));
            foreach (object? friend in dyn.Friends)
            {
                Console.WriteLine(friend);
            }

            try
            {
                dyn.Friends.Add(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var getTomorrow = (DateTime d) => d.AddDays(1.51).ToString();
            dyn.GetTomorrow = getTomorrow;
            try
            {
                Console.WriteLine(dyn.GetTomorrow(DateTime.Now));
                Console.WriteLine(dyn.GetTomorrow1(DateTime.Now));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine();
        }
    }

    class WroxDynamicObject : DynamicObject
    {
        private Dictionary<string, object?> membersDict = new Dictionary<string, object?>();

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            membersDict[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            return membersDict.TryGetValue(binder.Name, out result);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            try
            {
                object? method = membersDict[binder.Name];
                if (method is Delegate d)
                {
                    result = d.DynamicInvoke(args);
                    return true;
                }
                else if (method != null && args != null && args.Length == 1)
                {
                    dynamic dyn = method;
                    result = dyn(args[0]);
                    return true;
                }
                else
                {
                    return failure(out result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return failure(out result);
            }

            static bool failure(out object? result)
            {
                result = null;
                return false;
            }
        }
    }
}