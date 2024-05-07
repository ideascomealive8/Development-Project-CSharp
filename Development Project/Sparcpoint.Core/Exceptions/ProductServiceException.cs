using System;

namespace Sparcpoint.Exceptions
{
    public class ProductServiceException : Exception
    {
        public ProductServiceException(string message) : base(message)
        { }
    }
}