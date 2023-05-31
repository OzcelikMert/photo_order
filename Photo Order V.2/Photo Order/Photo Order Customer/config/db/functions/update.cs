using System;
using MySqlConnector;

namespace Photo_Order_Customer.config.db.functions
{
    class Update {
        private MySqlConnection dbConnection { get; set; }
        public void setBasket(long id, int count) {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("Update {0} ", AppLibrary.db.tables.Basket.TableName);
                    dbCommand.CommandText += String.Format("set {0}=@_2 ", AppLibrary.db.tables.Basket.count);
                    dbCommand.CommandText += String.Format("where {0}=@_1", AppLibrary.db.tables.Basket.id);
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", count);
                    dbCommand.ExecuteNonQuery();
                }
                this.dbConnection.Close();
            }
        }
    }
}
