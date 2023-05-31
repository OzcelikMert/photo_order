namespace AppLibrary.db.tables
{
    public class OrderProducts {
        public static string TableName { get { return "orderProducts"; } }
        public static string id { get { return "orderProductId"; } }
        public static string count { get { return "orderProductCount"; } }
        public static string productId { get { return "orderProductProductId"; } }
        public static string orderId { get { return "orderProductOrderId"; } }
        public static string priceTurkishLira { get { return "orderProductPriceTurkishLira"; } }
        public static string priceDollar { get { return "orderProductPriceDollar"; } }
        public static string priceEuro { get { return "orderProductPriceEuro"; } }
        public static string pricePound { get { return "orderProductPricePound"; } }
    }
}
