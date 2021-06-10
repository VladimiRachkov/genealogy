using Genealogy.Models;
using Genealogy.Repository.Abstract;
using System.Linq;

namespace Genealogy.Repository.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }

        /// <summary>
        /// Получить учетную запись по логину
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetByUsername(string username)
        {
            return _dbContext.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        /// <summary>
        /// Проверить существование учетной записи по логину
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckUsername(string username)
        {
            return _dbContext.Users.Any(u => u.Username == username);
        }

        /// <summary>
        /// Получить общее кол-во зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        public int GetUserAmount()
        {
            return _dbContext.Users.Count();
        }


        /// <summary>
        /// Получить учетную запись по почте
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

                /// <summary>
        /// Проверить существование учетной записи по почте
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckByEmail(string email)
        {
            return _dbContext.Users.Any(u => u.Email == email);
        }
        
    }
}