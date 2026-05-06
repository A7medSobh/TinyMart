using System;
using System.Collections.Generic;
using System.Text;

namespace TinyMart
{
    public class CartOverflowException : Exception
    {
        public CartOverflowException(string message) : base(message) { }
    }

    public class CartUnderflowException : Exception
    {
        public CartUnderflowException(string message) : base(message) { }
    }

}
