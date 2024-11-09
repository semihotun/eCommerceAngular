using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.ShowCases.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ShowCaseMapper
    {
        public static partial ShowCase CreateShowCaseCommandToShowCase(CreateShowCaseCommand showCase);
        public static partial ShowCase UpdateShowCaseCommandToShowCase(UpdateShowCaseCommand showCase);
    }
}