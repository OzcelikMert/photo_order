namespace AppLibrary.db.tables {
    public class Basket {
        public static string TableName { get { return "basket"; } }
        public static string id { get { return "basketId"; } }
        public static string count { get { return "basketCount"; } }
        public static string productId { get { return "basketProductId"; } }
        public static string customerId { get { return "basketCustomerId"; } }
        public static string createDate { get { return "basketCreateDate"; } }
    }
}
