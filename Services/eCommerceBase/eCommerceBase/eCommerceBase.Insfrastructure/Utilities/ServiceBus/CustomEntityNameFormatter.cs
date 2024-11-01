﻿using MassTransit;

namespace eCommerceBase.Insfrastructure.Utilities.ServiceBus
{
    /// <summary>
    /// MassTransit Entity Formatter
    /// </summary>
    public class CustomEntityNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
        {
            return typeof(T).Name.Underscore();
        }
    }
}
