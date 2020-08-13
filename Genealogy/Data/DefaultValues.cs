using System;

namespace Genealogy.Data
{
    public static class DefaultValues
    {
        public static class Roles
        {
            public static class Admin
            {
                private static Guid _id = new Guid("0CE9A36A-7E67-4E66-AA25-1DE6F6462E09");
                /// <summary>
                /// Уровень доступа: Администратор
                /// </summary>
                public static Guid Id { get { return _id; } }
            }
            public static class User
            {
                private static Guid _id = new Guid("E1731D50-21EF-45B1-9B77-EE70AA54D6E5");
                /// <summary>
                /// Уровень доступа: Просмотр
                /// </summary>
                public static Guid Id { get { return _id; } }
            }
        }
    }
}