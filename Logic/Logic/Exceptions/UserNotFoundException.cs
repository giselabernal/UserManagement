using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public string UserName { get; set; }    
        public UserNotFoundException() { }

        public UserNotFoundException(string message)
            : base(message) { }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        
    }
}
