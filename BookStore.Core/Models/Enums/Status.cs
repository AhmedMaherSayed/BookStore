using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models.Enums
{
    public enum Status
    {
        Pending = 1,
        Refunded,
        Delivered,
        Canceled,
        Placed,
        Processing,
        Returned,

    }
}
