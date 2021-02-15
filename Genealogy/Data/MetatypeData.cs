using System;

namespace Genealogy.Data
{
    public static class MetatypeData
    {
        public static class Product
        {
            private static Guid _id = new Guid("0814f9d0-00b5-4cce-bdc0-a60f99f9936c");
            /// <summary>
            /// Уровень доступа: Администратор
            /// </summary>
            public static Guid Id { get { return _id; } }
        }
    }
}