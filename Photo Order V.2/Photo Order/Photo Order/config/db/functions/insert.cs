using System;
using MySqlConnector;

namespace Photo_Order.config.db.functions
{
    class Insert {
        private MySqlConnection dbConnection { get; set; }

        public long setProductGroups(
            long ownerId,
            long eventId,
            DateTime createDate,
            bool isSliderActive,
            double priceTurkishLira,
            double priceDollar,
            double priceEuro,
            double pricePound) {
            long insertId = 0;
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("INSERT INTO `{0}` (" +
                        "`{1}`," +
                        "`{2}`," +
                        "`{3}`," +
                        "`{4}`," +
                        "`{5}`," +
                        "`{6}`," +
                        "`{7}`," +
                        "`{8}`" +
                        ") values (" +
                        "@_1," +
                        "@_2," +
                        "@_3," +
                        "@_4," +
                        "@_5," +
                        "@_6," +
                        "@_7," +
                        "@_8" +
                        ")", AppLibrary.db.tables.ProductGroups.TableName,
                        AppLibrary.db.tables.ProductGroups.ownerId,
                        AppLibrary.db.tables.ProductGroups.eventId,
                        AppLibrary.db.tables.ProductGroups.createDate,
                        AppLibrary.db.tables.ProductGroups.isSliderActive,
                        AppLibrary.db.tables.ProductGroups.priceTurkishLira,
                        AppLibrary.db.tables.ProductGroups.priceDollar,
                        AppLibrary.db.tables.ProductGroups.priceEuro,
                        AppLibrary.db.tables.ProductGroups.pricePound);

                    dbCommand.Parameters.AddWithValue("@_1", ownerId);
                    dbCommand.Parameters.AddWithValue("@_2", eventId);
                    dbCommand.Parameters.AddWithValue("@_3", AppLibrary.Var.toStringDateFormatDB(createDate));
                    dbCommand.Parameters.AddWithValue("@_4", isSliderActive);
                    dbCommand.Parameters.AddWithValue("@_5", priceTurkishLira);
                    dbCommand.Parameters.AddWithValue("@_6", priceDollar);
                    dbCommand.Parameters.AddWithValue("@_7", priceEuro);
                    dbCommand.Parameters.AddWithValue("@_8", pricePound);

                    dbCommand.ExecuteNonQuery();
                    insertId = dbCommand.LastInsertedId;
                }
                this.dbConnection.Close();
            }
            return insertId;
        }
        public void setProducts(
            long groupId,
            string[] images,
            string[] imagesHigh,
            bool isSliderActive
        ) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("INSERT INTO `{0}` (" +
                        "`{1}`," +
                        "`{2}`," +
                        "`{3}`," +
                        "`{4}`" +
                        ") values ", AppLibrary.db.tables.Products.TableName,
                        AppLibrary.db.tables.Products.groupId,
                        AppLibrary.db.tables.Products.image,
                        AppLibrary.db.tables.Products.imageHigh,
                        AppLibrary.db.tables.Products.isSliderActive);
                    int index = 0;
                    foreach (var image in images) {
                        dbCommand.CommandText += String.Format("(" +
                            "@_{0}_1," +
                            "@_{0}_2," +
                            "@_{0}_3," +
                            "@_{0}_4" +
                            "),", index);

                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_1", index), groupId);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_2", index), image);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_3", index), imagesHigh[index]);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_4", index), isSliderActive);

                        index++;
                    }
                    // Added (;) as soon as remove last character (,)
                    dbCommand.CommandText = dbCommand.CommandText.Substring(0, dbCommand.CommandText.Length - 1) + ";";
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setProductOwner(string image, string name) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("INSERT INTO `{0}` (" +
                        "`{1}`," +
                        "`{2}`," +
                        "`{3}`" +
                        ") values (" +
                        "@_1," +
                        "@_2," +
                        "@_3" +
                        ")", AppLibrary.db.tables.ProductOwner.TableName,
                        AppLibrary.db.tables.ProductOwner.image,
                        AppLibrary.db.tables.ProductOwner.name,
                        AppLibrary.db.tables.ProductOwner.createDate);
                    dbCommand.Parameters.AddWithValue("@_1", image);
                    dbCommand.Parameters.AddWithValue("@_2", name);
                    dbCommand.Parameters.AddWithValue("@_3", AppLibrary.Var.toStringDateFormatDB(DateTime.Now));

                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setEvents(string image, string name) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("INSERT INTO `{0}` (" +
                        "`{1}`," +
                        "`{2}`," +
                        "`{3}`" +
                        ") values (" +
                        "@_1," +
                        "@_2," +
                        "@_3" +
                        ")", AppLibrary.db.tables.Events.TableName,
                        AppLibrary.db.tables.Events.image,
                        AppLibrary.db.tables.Events.name,
                        AppLibrary.db.tables.Events.createDate);
                    dbCommand.Parameters.AddWithValue("@_1", image);
                    dbCommand.Parameters.AddWithValue("@_2", name);
                    dbCommand.Parameters.AddWithValue("@_3", AppLibrary.Var.toStringDateFormatDB(DateTime.Now));

                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
    }
}
