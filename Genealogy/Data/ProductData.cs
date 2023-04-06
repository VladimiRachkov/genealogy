using System;

namespace Genealogy.Data
{
    public class ProductData
    {
        public static class Subscription
        {
            private static Guid _id = new Guid("80445ce3-54b6-4fc2-ba36-ceec8ca79faf");
            public static Guid Id { get { return _id; } }

            private static string _name = new String("SUBSCRIBLE");
            public static string Name { get { return _name; } }
        }

        public static class Book
        {
            private static Guid _id = new Guid("b0263094-e3b2-499d-87b5-425446e71ed6");
            public static Guid Id { get { return _id; } }

            private static string _name = new String("BOOK");
            public static string Name { get { return _name; } }
        }
    }
}
