namespace AppLibrary.db.tables
{
    public class Orders {
        public static string TableName { get { return "orders"; } }
        public static string id { get { return "orderId"; } }
        public static string customerId { get { return "orderCustomerId"; } }
        public static string createDate { get { return "orderCreateDate"; } }
        public static string isPaid { get { return "orderIsPaid"; } }
        public static string paymentType { get { return "orderPaymentType"; } }
    }
}
