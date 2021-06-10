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
    }
}