namespace MVCSampleApp
{
    public static class TypeHelper
    {
        public static IEnumerable<string> GetPropertyValues<T>(this T obj)
        {
            return from p in typeof(T).GetProperties()
                   select $"{p.Name}: {p.GetValue(obj)}";
        }
    }
}
