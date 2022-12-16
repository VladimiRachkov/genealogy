using System;

namespace Genealogy.Data
{
    public class ProductData
    {
        public static class Subscribe
        {
            private static Guid _id = new Guid("80445ce3-54b6-4fc2-ba36-ceec8ca79faf");
            /// <summary>
            /// Метатип Подписка
            /// </summary>
            public static Guid Id { get { return _id; } }
        }

        public static class Book
        {
            private static Guid _id = new Guid("b0263094-e3b2-499d-87b5-425446e71ed6");
            /// <summary>
            /// Метатип Книга
            /// </summary>
            public static Guid Id { get { return _id; } }
        }
    }
}
