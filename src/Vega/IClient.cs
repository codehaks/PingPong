using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vega
{
    interface IClient
    {
        Task<string> SendAsync(string message);
    }
}
