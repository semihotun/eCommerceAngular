using eCommerceBase.Application.Handlers.ShowCases.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ShowCaseMapper
    {
        public static partial ShowCase CreateShowCaseCommandToShowCase(CreateShowCaseCommand showCase);
        public static partial ShowCase UpdateShowCaseCommandToShowCase(UpdateShowCaseCommand showCase);
    }
}