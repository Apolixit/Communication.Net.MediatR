using MediatR;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Domain.Notification
{
    /// <summary>
    /// Notification caught by console app
    /// </summary>
    public class BackgroundWorkerNotification : INotification
    {
        public string AnotherMessage { get; set; } = string.Empty;
    }
}
