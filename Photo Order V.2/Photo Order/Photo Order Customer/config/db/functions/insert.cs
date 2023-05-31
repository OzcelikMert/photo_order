using System;
using System.Collections.Generic;
using MySqlConnector;

namespace Photo_Order_Customer.config.db.functions
{
    class Insert {
        private MySqlConnection dbConnection { get; set; }

        public long setCustomer(string name, string email, string room, string password) {
            long insertId = 0;
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("INSERT INTO `{0}` (" +
                        "`{1}`," +
                        "`{2}`," +
                        "`{3}`," +
                        "`{4}`," +
                        "`{5}`" +
                        ") values (" +
                        "@_1," +
                        "@_2," +
                        "@_3," +
                        "@_4," +
                        "@_5" +
                        ")", AppLibrary.db.tables.Customers.TableName,
                        AppLibrary.db.tables.Customers.name,
                        AppLibrary.db.tables.Customers.email,
                        AppLibrary.db.tables.Customers.room,
                        AppLibrary.db.tables.Customers.password,
                        AppLibrary.db.tables.Customers.createDate);

                    dbCommand.Parameters.AddWithValue("@_1", name);
                    dbCommand.Parameters.AddWithValue("@_2", email);
                    dbCommand.Parameters.AddWithValue("@_3", room);
                    dbCommand.Parameters.AddWithValue("@_4", password);
                    dbCommand.Parameters.AddWithValue("@_5", (DateTime.Now).ToString("yyyy-MM-dd"));

                    dbCommand.ExecuteNonQuery();
                    insertId = dbCommand.LastInsertedId;
                }
                this.dbConnection.Close();
            }
            return insertId;
        }
        public long setBasket(long productId, long customerId, int count = 1) {
            long insertId = 0;
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("INSERT INTO `{0}` (" +
                        "`{1}`," +
                        "`{2}`," +
                        "`{3}`," +
                        "`{4}`" +
                        ") values (" +
                        "@_1," +
                        "@_2," +
                        "@_3," +
                        "@_4" +
                        ")", AppLibrary.db.tables.Basket.TableName,
                        AppLibrary.db.tables.Basket.productId,
                        AppLibrary.db.tables.Basket.customerId,
                        AppLibrary.db.tables.Basket.count,
                        AppLibrary.db.tables.Basket.createDate);
                    dbCommand.Parameters.AddWithValue("@_1", productId);
                    dbCommand.Parameters.AddWithValue("@_2", customerId);
                    dbCommand.Parameters.AddWithValue("@_3", count);
                    dbCommand.Parameters.AddWithValue("@_4", (DateTime.Now).ToString("yyyy-MM-dd"));

                    dbCommand.ExecuteNonQuery();
                    insertId = dbCommand.LastInsertedId;
                }
                this.dbConnection.Close();
            }
            return insertId;
        }
        public long setOrder(long customerId) {
            long insertId = 0;
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
                        ")", AppLibrary.db.tables.Orders.TableName,
                        AppLibrary.db.tables.Orders.customerId,
                        AppLibrary.db.tables.Orders.createDate,
                        AppLibrary.db.tables.Orders.isPaid);
                    dbCommand.Parameters.AddWithValue("@_1", customerId);
                    dbCommand.Parameters.AddWithValue("@_2", (DateTime.Now).ToString("yyyy-MM-dd"));
                    dbCommand.Parameters.AddWithValue("@_3", 0);

                    dbCommand.ExecuteNonQuery();
                    insertId = dbCommand.LastInsertedId;
                }
                this.dbConnection.Close();
            }
            return insertId;
        }
        public void setOrderProduct(List<OrderProduct> orderProducts) {
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
                        "`{7}`" +
                        ") values ", AppLibrary.db.tables.OrderProducts.TableName,
                        AppLibrary.db.tables.OrderProducts.count,
                        AppLibrary.db.tables.OrderProducts.productId,
                        AppLibrary.db.tables.OrderProducts.orderId,
                        AppLibrary.db.tables.OrderProducts.priceTurkishLira,
                        AppLibrary.db.tables.OrderProducts.priceDollar,
                        AppLibrary.db.tables.OrderProducts.priceEuro,
                        AppLibrary.db.tables.OrderProducts.pricePound);
                    int index = 0;
                    foreach(var orderProduct in orderProducts) {
                        dbCommand.CommandText += String.Format("(" +
                            "@_{0}_1," +
                            "@_{0}_2," +
                            "@_{0}_3," +
                            "@_{0}_4," +
                            "@_{0}_5," +
                            "@_{0}_6," +
                            "@_{0}_7" +
                            "),", index);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_1", index), orderProduct.count);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_2", index), orderProduct.productId);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_3", index), orderProduct.orderId);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_4", index), orderProduct.priceTurkishLira);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_5", index), orderProduct.priceDollar);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_6", index), orderProduct.priceEuro);
                        dbCommand.Parameters.AddWithValue(String.Format("@_{0}_7", index), orderProduct.pricePound);
                        index++;
                    }
                    // Added (;) as soon as remove last character (,)
                    dbCommand.CommandText = dbCommand.CommandText.Substring(0, dbCommand.CommandText.Length - 1) + ";";
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }

        public class OrderProduct {
            public int count { get; set; }
            public long productId { get; set; }
            public long orderId { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
        }
    }
}
