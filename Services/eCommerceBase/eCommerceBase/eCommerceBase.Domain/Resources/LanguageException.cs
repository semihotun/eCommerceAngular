using System.Globalization;
using System.Resources;

namespace eCommerceBase.Domain.Resources
{
    public static class LanguageException
    {
        private static readonly ResourceManager ResourceManager = new("eCommerceBase.Domain.Resources.Message", typeof(LanguageException).Assembly);

        public static string GetKey(string errorKey)
        {
            CultureInfo currentCulture = new("tr");
            return ResourceManager.GetString(errorKey, currentCulture) ?? errorKey;
        }
    }
}
