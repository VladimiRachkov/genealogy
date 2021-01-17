using System;

namespace Genealogy.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Соль пароля
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTime FinishDate { get; set; }

        /// <summary>
        /// Идентификтор роли
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        /// <value></value>
        public Role Role { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        /// <value></value>
        public string Email { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        /// <value></value>
        public string Status { get; set; }

    }
}

