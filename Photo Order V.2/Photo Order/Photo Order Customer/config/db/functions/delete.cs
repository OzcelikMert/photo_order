using System;
using MySqlConnector;

namespace Photo_Order_Customer.config.db.functions
{
    class Delete {
        private MySqlConnection dbConnection { get; set; }

        public void setBasket(long id = 0, long customerId = 0) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Delete from {0} where {1} = @_1", AppLibrary.db.tables.Basket.TableName, (customerId > 0) ? AppLibrary.db.tables.Basket.customerId : AppLibrary.db.tables.Basket.id);
                    dbCommand.Parameters.AddWithValue("@_1", (customerId > 0) ? customerId : id);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
        public void setCustomer(long id = 0) {
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
    }
}
