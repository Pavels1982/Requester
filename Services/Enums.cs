using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requester.Services
{
    public class Enums
    {

        public enum Status : byte { ReadyToPost = 0, Process = 1 }
       
        public enum ValidationStatus : byte { Process = 0, Passed = 1, Denied  = 2}

    }
}
