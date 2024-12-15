using eCommerceBase.Application.Handlers.Currencies.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class CurrencyMapper
    {
        public static partial Currency CreateCurrencyCommandToCurrency(CreateCurrencyCommand currency);
        public static partial Currency UpdateCurrencyCommandToCurrency(UpdateCurrencyCommand currency);
    }
}