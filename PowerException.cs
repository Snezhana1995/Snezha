using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wars
{
    class PowerException: Exception
    {
        public PowerException(String message) : base(message)
        {
        }
    }
}
