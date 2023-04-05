using System;

namespace Genealogy.Data
{
    public class Users
    {
        /// <summary>
        /// Админ
        /// </summary>
        public static class Admin
        {
            private static Guid _id = new Guid("af1bf4e2-54e8-44a6-92ca-21b33d2d82ef");
            public static Guid Id { get { return _id; } }
        }
    }
}