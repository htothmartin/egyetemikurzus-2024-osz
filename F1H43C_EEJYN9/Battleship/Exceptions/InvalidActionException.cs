using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1H43C_EEJYN9.Exceptions
{
    public class InvalidActionException : Exception
    {
        public InvalidActionException(): base("Az a lépés nem szabályos."){}

        public InvalidActionException(string message) : base(message) { }

    }
}
