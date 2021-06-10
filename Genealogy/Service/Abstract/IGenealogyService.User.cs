
using Genealogy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        /// <summary>
        /// Аутентификация пользователей
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Authenticate(AuthenticateUserDto userDto);

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(Guid id);

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User CreateUser(User user, string password);

        List<UserDto> GetUser(UserFilter filter);

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userParam"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User UpdateUser(User userParam);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RemoveUser(Guid id);

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Изменить статус пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool ChangeUserStatus(Guid userId, string status);

        /// <summary>
        /// Получение кол-ва зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        int GetUserAmount();

        /// <summary>
        /// Проверка есть ли у пользователя права администратора
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool CheckAdminByUserId(Guid userId);

        Guid GetCurrentUserId();
    }
}