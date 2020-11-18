using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
