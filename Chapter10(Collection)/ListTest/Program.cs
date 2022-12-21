using ReflectionLib;
using System.Reflection;

namespace ListTest
{

    /// <summary>
    /// 测试编译器是如何实现本地函数的，得出结论如下：
    /// 1. lambda和匿名本地函数只是语法不同，实现上都是一样的思路
    /// 2. 具名的本地函数也只是有名字的本地函数
    /// 3. lambda/匿名本地函数/具名本地函数都可以是静态或非静态
    /// 4. 非静态的本地函数可以访问并修改本地变量、类中的静态变量，如果宿主方法是非静态的，则再加上类中的非静态变量
    /// 5. 非静态的本地函数，编译器有两种实现方式：
    /// 5.1 需要够访问本地变量的版本：生成一个匿名的嵌套类，类中包含的内容：外部对象的一个引用（用来访问外部类中的非静态成员, 宿主方法是静态的情况下没有此字段），
    /// 所有需要访问的本地变量的引用变量，方法成员囊括宿主方法中所有的非静态的本地函数
    /// 5.2 不需要够访问本地变量的版本：直接在宿主方法所在的类中生成一个非静态的方法
    /// 5.3 如果宿主方法中同时拥有5.1和5.2中描述的本地方法，编译器会把所有非静态本地方法（无论是否使用的本地变量）都放在生成的匿名的嵌套类中
    /// 6. 静态的本地函数只能访问类中的静态成员，相当于只具有本地作用域的类中静态方法
    /// 7. 多播的委托看不出来如何实现的
    /// </summary>
    internal class Program
    {
        private static string _country = "JAP";
        private int _id = 0;

        void fun()
        {
            int local = 0;

            static void staticInner()
            {
                _country = _country + "(inner fun modified)";
            }

            //void innerWithLocalAccess() 
            //{
            //    local++;
            //    _id++;
            //}

            void innerWithoutLocalAccess()
            {
                _id++;
            }

            var staticInnerDelegate = staticInner;
            //var innerWithLocalAccessDelegate = innerWithLocalAccess;
            var innerWithoutLocalAccessDelegate = innerWithoutLocalAccess;
            Console.WriteLine("staticInnerDelegate: " + GetInfo(staticInnerDelegate));
            //Console.WriteLine("innerWithLocalAccessDelegate: " + GetInfo(innerWithLocalAccessDelegate));
            Console.WriteLine("innerWithoutLocalAccessDelegate: " + GetInfo(innerWithoutLocalAccessDelegate));
            Console.WriteLine($"Before invoke delegates: _country={_country}, _id={_id}, local={local}");
            staticInnerDelegate();
            //innerWithLocalAccessDelegate();
            innerWithoutLocalAccessDelegate();
            Console.WriteLine($"After invoke delegates: _country={_country}, _id={_id}, local={local}");
            Console.WriteLine();


            var multicaseDelegate = (Racer racer) => true;
            multicaseDelegate += new FindCountry(ref _country).FindCountryPredicate;
            Console.WriteLine("multicaseDelegate: " + GetInfo(multicaseDelegate));
            Console.WriteLine();

            var multicaseDelegate2 = new FindCountry(ref _country).FindCountryPredicate;
            multicaseDelegate2 += (Racer racer) => true;
            Console.WriteLine("multicaseDelegate2: " + GetInfo(multicaseDelegate2));
        }

        static void Main(string[] args)
        {
            List<Racer> list = new List<Racer>
            {
                new Racer(0, "Deng", "Yingwei", "USA"),
                new Racer(0, "Chen", "Lu", "JAP")
            };

            string country = "JAP";
            FindCountry findCountryObj = new FindCountry(ref country);
            Predicate<Racer> objMethod = findCountryObj.FindCountryPredicate;
            Console.WriteLine($"objMethod: Target == findCountryObj ? {objMethod.Target == findCountryObj}, " + GetInfo(objMethod));
            int find1 = list.FindIndex(objMethod);
            Console.WriteLine($"find1 = {find1}");
            Console.WriteLine();


            Predicate<Racer> lambda = (Racer racer) => racer.Country == country;
            Console.WriteLine("lambda: " + GetInfo(lambda));
            int find2 = list.FindIndex(lambda);
            Console.WriteLine($"find2 = {find2}");
            Console.WriteLine();


            Predicate<Racer> anonymous = delegate (Racer racer)
            {
                return racer.Country == country;
            };
            Console.WriteLine("anonymous: " + GetInfo(anonymous));
            Console.WriteLine($"ReferenceEquals(anonymous.Target, lambda.Target)={ReferenceEquals(anonymous.Target, lambda.Target)}");
            int find3 = list.FindIndex(anonymous);
            Console.WriteLine($"find3 = {find3}");
            Console.WriteLine();


            static bool staticLocal(Racer racer) => racer.Country == _country; // 相当于只能在本地访问的静态方法
            Predicate<Racer> staticLocalMethod = staticLocal;
            Console.WriteLine("staticLocalMethod: " + GetInfo(staticLocalMethod));
            Console.WriteLine($"ReferenceEquals(staticLocalMethod.Target, lambda.Target)={ReferenceEquals(staticLocalMethod.Target, lambda.Target)}");
            Console.WriteLine($"staticLocalMethod.Target == null ? {staticLocalMethod.Target == null}");
            int find4 = list.FindIndex(staticLocalMethod);
            Console.WriteLine($"find4 = {find4}");
            Console.WriteLine();


            Predicate<Racer> staticInClassMethod = staticInClass;
            Console.WriteLine("staticInClassMethod: " + GetInfo(staticInClassMethod));
            Console.WriteLine($"ReferenceEquals(staticInClassMethod.Target, lambda.Target)={ReferenceEquals(staticInClassMethod.Target, lambda.Target)}");
            Console.WriteLine($"staticInClassMethod.Target == null ? {staticInClassMethod.Target == null}");
            int find5 = list.FindIndex(staticInClassMethod);
            Console.WriteLine($"find4 = {find5}");
            Console.WriteLine();


            country = "USA";
            _country = "USA";
            Console.WriteLine("After Chanage country and _country to USA:");
            find1 = list.FindIndex(objMethod);
            find2 = list.FindIndex(lambda);
            find3 = list.FindIndex(anonymous);
            find4 = list.FindIndex(staticLocalMethod);
            find5 = list.FindIndex(staticInClassMethod);
            Console.WriteLine($"find1 = {find1}, find2 = {find2}, find3 = {find3}, find4 = {find4}, find5 = {find5}");
            Console.WriteLine();


            new Program().fun();
            Console.WriteLine();
        }

        static bool staticInClass(Racer racer) => racer.Country == _country;

        private static string GetInfo(Delegate predicate)
        {
            object? target = predicate.Target;
            return $"Target={target}({target?.GetTypeInfo()}),\nMethod={predicate.Method}";
        }
    }

    class FindCountry
    {
        private string[] country;

        public string Country => country[0];

        public FindCountry(ref string country)
        {
            this.country = new string[] { country };
        }

        public bool FindCountryPredicate(Racer racer) => racer.Country == Country;
    }

    //class FindCoutryRef
    //{
    //    private ref string country;
    //}
}