namespace AppLibrary.db.tables
{
    public class Products {
        public static string TableName { get { return "products"; } }
        public static string id { get { return "productId"; } }
        public static string groupId { get { return "productProductGroupId"; } }
        public static string image { get { return "productImage"; } }
        public static string imageHigh { get { return "productImageHigh"; } }
        public static string isSliderActive { get { return "productIsSliderActive"; } }
    }
}
