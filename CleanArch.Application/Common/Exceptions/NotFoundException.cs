using System;

namespace Application.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"La propiedad {name} : Con el valor  '({key})' no se encontró")
        {
        }
        public NotFoundException(string name)
            : base(name)
        {
        }
    }
}