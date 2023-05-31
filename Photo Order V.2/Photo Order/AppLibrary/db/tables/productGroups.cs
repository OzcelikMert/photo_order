namespace AppLibrary.db.tables
{
    public class ProductGroups {
        public static string TableName { get { return "productGroups"; } }
        public static string id { get { return "productGroupId"; } }
        public static string ownerId { get { return "productGroupOwnerId"; } }
        public static string eventId { get { return "productGroupEventId"; } }
        public static string createDate { get { return "productGroupCreateDate"; } }
        public static string priceTurkishLira { get { return "productGroupPriceTurkishLira"; } }
        public static string priceDollar { get { return "productGroupPriceDollar"; } }
        public static string priceEuro { get { return "productGroupPriceEuro"; } }
        public static string pricePound { get { return "productGroupPricePound"; } }
        public static string isSliderActive { get { return "productGroupIsSliderActive"; } }
    }
}
