using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requester.Services
{
    public static class UnixTimeHelper
    {
        public static int UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow.ToLocalTime() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (int)timeSpan.TotalSeconds;
        }

    }
}
