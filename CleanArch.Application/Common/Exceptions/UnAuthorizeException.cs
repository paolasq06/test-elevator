using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Application.Core.Exceptions
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException(string message) : base(message)
        {
           
        }
    }
}
