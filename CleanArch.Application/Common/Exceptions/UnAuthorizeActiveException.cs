using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    class UnAuthorizeActiveException : Exception
    {
        public UnAuthorizeActiveException()
            : base("El usuario se encuentra inactivo")
        {

        }
    }
}
