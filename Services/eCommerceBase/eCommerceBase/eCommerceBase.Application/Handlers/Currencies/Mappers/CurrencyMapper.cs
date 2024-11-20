using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.Currencies.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class CurrencyMapper
    {
        public static partial Currency CreateCurrencyCommandToCurrency(CreateCurrencyCommand currency);
        public static partial Currency UpdateCurrencyCommandToCurrency(UpdateCurrencyCommand currency);
    }
}