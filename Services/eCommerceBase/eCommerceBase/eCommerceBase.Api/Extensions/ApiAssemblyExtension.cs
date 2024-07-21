using eCommerceBase.Application.Assemblies;
using eCommerceBase.Domain.Assemblies;
using eCommerceBase.Insfrastructure.Utilities.Assemblies;
using System.Reflection;

namespace eCommerceBase.Extensions
{
    public static class ApiAssemblyExtensions
    {
        public static Assembly[] GetLibrariesAssemblies()
        {
            var application = ApplicationAssemblyExtension.GetApplicationAssembly();
            var domain = DomainAssemblyExtension.GetDomainAssembly();
            var insfrastructure = InsfrastructureAssemblyExtension.GetInsfrastructureAssembly();
            return [application, domain, insfrastructure];
        }
    }
}
