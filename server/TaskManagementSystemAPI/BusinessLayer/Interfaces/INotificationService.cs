using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INotificationService
    {
        public void SendEmailAsync(string email, string subject, string message);

    }
}
