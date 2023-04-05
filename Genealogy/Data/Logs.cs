using System;


namespace Genealogy.Data
{
    public class Logs
    {
        /// <summary>
        /// Последний созданный лог
        /// </summary>
        public static class LastLog
        {
            private static Guid _id = new Guid("aa91e26c-5097-48e5-8015-f90380359dec");
            public static Guid Id { get { return _id; } }

            private static string _name = new String("LAST_LOG");
            public static string Name { get { return _name; } }
        }
    }
}