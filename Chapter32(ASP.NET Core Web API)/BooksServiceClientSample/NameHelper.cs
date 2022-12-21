using BooksServiceClientSample.Pages;

namespace BooksServiceClientSample
{
    public static class NameHelper
    {
        public static string PageNameOf<TPage>()
            where TPage : class
        {
            int prefix = "Pages_".Length;
            return typeof(TPage).Name[prefix..];
        }

        public static string PageNameOf(Type pageType)
        {
            int prefix = "Pages_".Length;
            return pageType.Name[prefix..];
        }
    }
}
