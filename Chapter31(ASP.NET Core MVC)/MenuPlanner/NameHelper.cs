namespace MenuPlanner
{
    public static class NameHelper
    {
        public static string ViewComponent(Type viewComponentType)
        {
            string typeName = viewComponentType.Name;
            int idx = typeName.IndexOf(nameof(ViewComponent));
            return typeName[0..idx];
        }

        public static string ViewComponent<TViewCompnent>()
            where TViewCompnent : class
        {
            return ViewComponent(typeof(TViewCompnent));
        }

        public static string Controller(Type controllerType)
        {
            string typeName = controllerType.Name;
            int idx = typeName.IndexOf(nameof(Controller));
            return typeName[0..idx];
        }

        public static string Controller<TController>()
            where TController : class
        {
            return Controller(typeof(TController));
        }
    }
}
