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

        public static class UserStatuses
        {
            private static string _notConfirmed = new String("NOT_CONFIRMED");
            private static string _actived = new String("ACTIVED");
            private static string _blocked = new String("BLOCKED");
            private static string _paid = new String("PAID");
            public static string NotConfirmed { get { return _notConfirmed; } }
            public static string Actived { get { return _actived; } }
            public static string Blocked { get { return _blocked; } }
            public static string Paid { get { return _paid; } }

        }
    }
}