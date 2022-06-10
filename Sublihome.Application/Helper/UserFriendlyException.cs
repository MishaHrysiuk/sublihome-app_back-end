using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sublihome.Application.Helper
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException()
            :base()
        {
            
        }

        public UserFriendlyException(string message)
            : base(message)
        {

        }

        public UserFriendlyException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
