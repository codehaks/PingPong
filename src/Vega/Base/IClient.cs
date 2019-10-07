using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vega.Base
{
    interface IClient
    {
        Task<string> SendAsync(string message);
    }
}
