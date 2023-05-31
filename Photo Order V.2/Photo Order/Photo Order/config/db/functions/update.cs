using System;
using MySqlConnector;

namespace Photo_Order.config.db.functions
{
    class Update {
        private MySqlConnection dbConnection { get; set; }
        public void setProductGroups(long id,
            long ownerId,
            long eventId,
            DateTime createDate,
            bool isSliderActive,
            double priceTurkishLira,
            double priceDollar,
            double priceEuro,
            double pricePound
        ) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} set " +
                        "{2}=@_2," +
                        "{3}=@_3," +
                        "{4}=@_4," +
                        "{5}=@_5," +
                        "{6}=@_6," +
                        "{7}=@_7," +
                        "{8}=@_8," +
                        "{9}=@_9" +
                        " where {1} = @_1", AppLibrary.db.tables.ProductGroups.TableName, AppLibrary.db.tables.ProductGroups.id,
                        AppLibrary.db.tables.ProductGroups.ownerId,
                        AppLibrary.db.tables.ProductGroups.eventId,
                        AppLibrary.db.tables.ProductGroups.createDate,
                        AppLibrary.db.tables.ProductGroups.isSliderActive,
                        AppLibrary.db.tables.ProductGroups.priceTurkishLira,
                        AppLibrary.db.tables.ProductGroups.priceDollar,
                        AppLibrary.db.tables.ProductGroups.priceEuro,
                        AppLibrary.db.tables.ProductGroups.pricePound);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", ownerId);
                    dbCommand.Parameters.AddWithValue("@_3", eventId);
                    dbCommand.Parameters.AddWithValue("@_4", AppLibrary.Var.toStringDateFormatDB(createDate));
                    dbCommand.Parameters.AddWithValue("@_5", isSliderActive);
                    dbCommand.Parameters.AddWithValue("@_6", priceTurkishLira);
                    dbCommand.Parameters.AddWithValue("@_7", priceDollar);
                    dbCommand.Parameters.AddWithValue("@_8", priceEuro);
                    dbCommand.Parameters.AddWithValue("@_9", pricePound);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setProduct(long id,
            string image,
            string imageHigh,
            bool isSliderActive
        ) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} set " +
                        "{2}=@_2," +
                        "{3}=@_3," +
                        "{4}=@_4" +
                        " where {1} = @_1", AppLibrary.db.tables.Products.TableName, AppLibrary.db.tables.Products.id,
                        AppLibrary.db.tables.Products.image,
                        AppLibrary.db.tables.Products.imageHigh,
                        AppLibrary.db.tables.Products.isSliderActive);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", image);
                    dbCommand.Parameters.AddWithValue("@_3", imageHigh);
                    dbCommand.Parameters.AddWithValue("@_4", isSliderActive);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setProductOwner(long id, string image, string name) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} set " +
                        "{2}=@_2," +
                        "{3}=@_3" +
                        " where {1} = @_1", AppLibrary.db.tables.ProductOwner.TableName, AppLibrary.db.tables.ProductOwner.id,
                        AppLibrary.db.tables.ProductOwner.image,
                        AppLibrary.db.tables.ProductOwner.name);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", image);
                    dbCommand.Parameters.AddWithValue("@_3", name);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setEvents(long id, string image, string name) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} set " +
                        "{2}=@_2," +
                        "{3}=@_3" +
                        " where {1} = @_1", AppLibrary.db.tables.Events.TableName, AppLibrary.db.tables.Events.id,
                        AppLibrary.db.tables.Events.image,
                        AppLibrary.db.tables.Events.name);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", image);
                    dbCommand.Parameters.AddWithValue("@_3", name);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setCustomer(long id, string password) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} ", AppLibrary.db.tables.Customers.TableName);
                    dbCommand.CommandText += String.Format("set {0}=@_2 ", AppLibrary.db.tables.Customers.password);
                    dbCommand.CommandText += String.Format("where {0}=@_1", AppLibrary.db.tables.Customers.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", password);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setOrder(long id, bool isPaid, string paymentType = "") {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} ", AppLibrary.db.tables.Orders.TableName);
                    dbCommand.CommandText += String.Format("set {0}=@_2,{1}=@_3 ", AppLibrary.db.tables.Orders.isPaid, AppLibrary.db.tables.Orders.paymentType);
                    dbCommand.CommandText += String.Format("where {0}=@_1", AppLibrary.db.tables.Orders.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", isPaid);
                    dbCommand.Parameters.AddWithValue("@_3", paymentType);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setOrderProduct(long id,
            double priceTurkishLira,
            double priceDollar,
            double priceEuro,
            double pricePound
        ) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} set ", AppLibrary.db.tables.OrderProducts.TableName);
                    dbCommand.CommandText += String.Format("{0}=@_2, ", AppLibrary.db.tables.OrderProducts.priceTurkishLira);
                    dbCommand.CommandText += String.Format("{0}=@_3, ", AppLibrary.db.tables.OrderProducts.priceDollar);
                    dbCommand.CommandText += String.Format("{0}=@_4, ", AppLibrary.db.tables.OrderProducts.priceEuro);
                    dbCommand.CommandText += String.Format("{0}=@_5 ", AppLibrary.db.tables.OrderProducts.pricePound);
                    dbCommand.CommandText += String.Format("where {0}=@_1 ", AppLibrary.db.tables.OrderProducts.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", priceTurkishLira);
                    dbCommand.Parameters.AddWithValue("@_3", priceDollar);
                    dbCommand.Parameters.AddWithValue("@_4", priceEuro);
                    dbCommand.Parameters.AddWithValue("@_5", pricePound);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
    }
}
