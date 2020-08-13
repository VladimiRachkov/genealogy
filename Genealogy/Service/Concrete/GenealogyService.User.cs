using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genealogy.Data;
using Genealogy.Helpers;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Genealogy.Service.Helpers;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        /// <summary>
        /// Аутентификация пользователей
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Authenticate(AuthenticateUserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
                return null;

            var currentUser = _unitOfWork.UserRepository.GetByEmail(userDto.Email);

            if (currentUser == null)
                return null;

            if (!VerifyPasswordHash(userDto.Password, currentUser.PasswordHash, currentUser.PasswordSalt))
                return null;

            return currentUser;
        }

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(Guid id)
        {
            return _unitOfWork.UserRepository.GetByID(id);
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User CreateUser(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Требуется ввести пароль.");

            if (_unitOfWork.UserRepository.CheckByEmail(user.Email))
            {
                throw new AppException("Пользователь с почтой " + user.Email + " уже зарегистрирован.");
            }
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.StartDate = DateConverter.ConvertToRTS(DateTime.UtcNow.ToLocalTime());

            // Учетная запись первого зарегистрированного пользователя должна быть подтверждена
            if (GetUserAmount() == 0)
            {
                user.IsConfirmed = true;
                user.Role = _unitOfWork.RoleRepository.GetRoleById(DefaultValues.Roles.Admin.Id);
            }
            else
            {
                user.Role = _unitOfWork.RoleRepository.GetRoleById(DefaultValues.Roles.User.Id);
            }

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Save();

            return user;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<UserDto> GetUser(UserFilter filter)
        {
            return _unitOfWork.UserRepository.Get(x =>
                (filter.Id != Guid.Empty ? x.Id == filter.Id : true &&
                filter.Username != null ? x.Username == filter.Username : true))
                .Select(i => _mapper.Map<UserDto>(i)).ToList();
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userParam"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User UpdateUser(User userParam, string password = null)
        {
            var user = _unitOfWork.UserRepository.GetByID(userParam.Id);

            if (user == null)
                throw new AppException("Пользователь не найден.");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_unitOfWork.UserRepository.CheckUsername(user.Username))
                    throw new AppException("Пользователь " + userParam.Username + " существует.");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
            user = _unitOfWork.UserRepository.GetByID(user.Id);
            return user;

        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveUser(Guid id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);
            if (user != null)
            {
                _unitOfWork.UserRepository.Delete(user);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Создание хэша пароля
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Пароль не может быть пустым или содержать пробелы.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Верификация хэша пароля
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.Get();
        }

        /// <summary>
        /// Изменить статус пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        public bool ChangeUserStatus(Guid userId, bool isConfirmed = false)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.IsConfirmed = isConfirmed;
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Получение кол-ва зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        public int GetUserAmount()
        {
            return _unitOfWork.UserRepository.GetUserAmount();
        }

        /// <summary>
        /// Проверка есть ли у пользователя права администратора
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckAdminByUserId(Guid userId)
        {
            return _unitOfWork.UserRepository.GetByID(userId).RoleId == DefaultValues.Roles.Admin.Id;
        }
    }
}