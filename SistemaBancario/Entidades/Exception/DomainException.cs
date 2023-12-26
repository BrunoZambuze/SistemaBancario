using System;

namespace SistemaBancario.Entidades.Exception
{
    //Exceção personalizada
    internal class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
