namespace AppLibrary.db.tables {
    public class Customers {
        public static string TableName { get { return "customers"; } }
        public static string id { get { return "customerId"; } }
        public static string name { get { return "customerName"; } }
        public static string email { get { return "customerEmail"; } }

        public static string room { get { return "customerRoom"; } }
        public static string password { get { return "customerPassword"; } }
        public static string createDate { get { return "customerCreateDate"; } }
    }
}
