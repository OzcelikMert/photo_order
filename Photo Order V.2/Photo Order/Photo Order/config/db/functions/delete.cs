using System;
using MySqlConnector;

namespace Photo_Order.config.db.functions
{
    class Delete {
        private MySqlConnection dbConnection { get; set; }
        public void setProductGroups(long id) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} where {1} = @_1", AppLibrary.db.tables.ProductGroups.TableName, AppLibrary.db.tables.ProductGroups.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setProduct(long id = 0, long groupId = 0) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} where {1} = @_1", AppLibrary.db.tables.Products.TableName, (groupId > 0) ? AppLibrary.db.tables.Products.groupId : AppLibrary.db.tables.Products.id);
                    dbCommand.Parameters.AddWithValue("@_1", (groupId > 0) ? groupId : id);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setProductOwner(long id) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} where {1} = @_1", AppLibrary.db.tables.ProductOwner.TableName, AppLibrary.db.tables.ProductOwner.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setEvents(long id) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} where {1} = @_1", AppLibrary.db.tables.Events.TableName, AppLibrary.db.tables.Events.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setCustomer(long id) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} where {1} = @_1", AppLibrary.db.tables.Customers.TableName, AppLibrary.db.tables.Customers.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setBasket(long customerId) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} where {1} = @_1", AppLibrary.db.tables.Basket.TableName, AppLibrary.db.tables.Basket.customerId);
                    dbCommand.Parameters.AddWithValue("@_1", customerId );
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setOrder(long id = 0, long customerId = 0) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} ", AppLibrary.db.tables.Orders.TableName);
                    dbCommand.CommandText += String.Format("where {0}=@_1", (id > 0) ? AppLibrary.db.tables.Orders.id : AppLibrary.db.tables.Orders.customerId);
                    dbCommand.Parameters.AddWithValue("@_1", (id > 0) ? id : customerId);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setOrderProduct(long id = 0, long orderId = 0, long customerId = 0, long productId = 0, long groupId = 0) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    string where = " where ";
                    dbCommand.CommandText = String.Format("Delete from {0} ", AppLibrary.db.tables.OrderProducts.TableName);
                    if (customerId > 0) {
                        dbCommand.CommandText += String.Format(where + " {0} IN ({1})", AppLibrary.db.tables.OrderProducts.orderId,
                            String.Format("select {2} from {0} where {1}=@_1", AppLibrary.db.tables.Orders.TableName, AppLibrary.db.tables.Orders.customerId, AppLibrary.db.tables.Orders.id)
                        );
                        dbCommand.Parameters.AddWithValue("@_1", customerId);
                        where = " and ";
                    }
                    if (groupId > 0) {
                        dbCommand.CommandText += String.Format(where + " {0} IN ({1})", AppLibrary.db.tables.OrderProducts.productId,
                            String.Format("select {2} from {0} where {1}=@_2", AppLibrary.db.tables.Products.TableName, AppLibrary.db.tables.Products.groupId, AppLibrary.db.tables.Products.id)
                        );
                        dbCommand.Parameters.AddWithValue("@_2", groupId);
                    } else if(productId > 0) {
                        dbCommand.CommandText += String.Format(where + " {0}=@_2", AppLibrary.db.tables.OrderProducts.productId);
                        dbCommand.Parameters.AddWithValue("@_2", productId);
                    } else {
                        dbCommand.CommandText += String.Format(where + " {0}=@_2", (id > 0) ? AppLibrary.db.tables.OrderProducts.id : AppLibrary.db.tables.OrderProducts.orderId);
                        dbCommand.Parameters.AddWithValue("@_2", (id > 0) ? id : orderId);
                    }
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
    }
}
