using System;
using System.Threading.Tasks;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Task SendEmailAsync(string subject, string message, string emailFrom, string name);
        void SendMessage(BusinessObjectInDto boDto);
        Task<bool> SendEmailToUser(string subject, string email, string message);
    }
}