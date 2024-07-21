using System.Reflection;

namespace eCommerceBase.Application.Assemblies
{
    public static class ApplicationAssemblyExtension
    {
        public static Assembly GetApplicationAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
