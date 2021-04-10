using System;

namespace Genealogy.Data
{
    public class Settings
    {
        /// <summary>
        /// Почтовый адрес для получения писем с сайта
        /// </summary>
        /// <returns></returns>
        private static string _adminEmail = new String("ADMIN_EMAIL");
        public static string AdminEmail { get { return _adminEmail; } }

        /// <summary>
        /// Адрес служебного почтового ящика для отправки писем с сайта
        /// </summary>
        /// <returns></returns>
        private static string _serviceEmail = new String("SERVICE_EMAIL");
        public static string ServiceEmail { get { return _serviceEmail; } }
        /// <summary>
        /// Пароль почтового ящика
        /// </summary>
        /// <returns></returns>
        private static string _serviceEmailPassword = new String("SERVICE_EMAIL_PASSWORD");
        public static string ServiceEmailPassword { get { return _serviceEmailPassword; } }

        /// <summary>
        /// Заголовок письма
        /// </summary>
        /// <returns></returns>
        private static string _emailTitle = new String("EMAIL_TITLE");
        public static string EmailTitle { get { return _emailTitle; } }

        /// <summary>
        /// Адрес SMTP сервера
        /// </summary>
        /// <returns></returns>
        private static string _smtpServer = new String("SMTP_SERVER");
        public static string SmtpServer { get { return _smtpServer; } }

        /// <summary>
        /// Порт SMTP сервера
        /// </summary>
        /// <returns></returns>
        private static string _smtpServerPort = new String("SMTP_SERVER_PORT");
        public static string SmtpServerPort { get { return _smtpServerPort; } }

        /// <summary>
        /// SSL на SMTP сервере
        /// </summary>
        /// <returns></returns>
        private static string _smtpServerSsl = new String("SMTP_SERVER_SSL");
        public static string SmtpServerSsl { get { return _smtpServerSsl; } }
    }
}