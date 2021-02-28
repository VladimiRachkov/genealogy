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

        public static class Purchase
        {
            private static Guid _id = new Guid("d7d7f8ed-8448-48c7-b10c-1d7f98748bf6");
            /// <summary>
            /// Уровень доступа: Администратор
            /// </summary>
            public static Guid Id { get { return _id; } }
        }
    }
}