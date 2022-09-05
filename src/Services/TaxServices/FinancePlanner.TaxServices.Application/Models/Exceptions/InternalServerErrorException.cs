﻿using System;
using System.Runtime.Serialization;

namespace FinancePlanner.TaxServices.Application.Models.Exceptions
{
    [Serializable]
    public class InternalServerErrorException : ApplicationException
    {
        public InternalServerErrorException() { }

        public InternalServerErrorException(Type type) : base($"{type} is missing") { }

        protected InternalServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public InternalServerErrorException(string message) : base(message) { }

        public InternalServerErrorException(string message, Exception innerException) : base(message, innerException) { }
    }
}