using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Utilities.Exceptions
{
    public class CategoryNotEmptyException : Exception
    {
        public CategoryNotEmptyException(string message) : base(message)
        {
        }
    }
}
