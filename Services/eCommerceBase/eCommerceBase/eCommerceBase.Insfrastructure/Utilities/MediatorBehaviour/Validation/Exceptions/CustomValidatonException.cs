﻿using Newtonsoft.Json;

namespace eCommerceBase.Insfrastructure.Utilities.MediatorBehaviour.Validation.Exceptions
{
    /// <summary>
    /// validation exception
    /// </summary>
    /// <param name="message"></param>
    public class CustomValidatonException(IEnumerable<ValidationData> message) : Exception
    {
        public override string Message { get; } = JsonConvert.SerializeObject(message);
    }
}
