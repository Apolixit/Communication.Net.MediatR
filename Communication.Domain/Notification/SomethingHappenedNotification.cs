using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Domain.Notification
{
    /// <summary>
    /// Notification dipatched to everyone
    /// </summary>
    public class SomethingHappenedNotification : INotification
    {
        public string Message { get; set; } = string.Empty;
    }
}
