using System;
using MySqlConnector;

namespace Photo_Order.config.db
{
    class Create {
        private MySqlConnection dbConnection { get; set; }
        public Create() { }
        public void init(){
            int affectedRows = this.createDatabase();
            this.createTables();
            if (affectedRows > 0) this.addIndexKeys();
        }
        private void setUpdateDBValues() {
            /*using (DBValues.ConnectDB = new MySqlConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                SELECT COUNT(*) AS CNTREC FROM pragma_table_info('tablename') WHERE name='column_name'
            }*/
        }
        private int createDatabase() {
            int result = 0;
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionStringWithoutDBName)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE DATABASE IF NOT EXISTS `{0}` CHARACTER SET utf8 COLLATE utf8_general_ci;", AppLibrary.Values.dbInfo.dbName);
                    result = dbCommand.ExecuteNonQuery();
                }
            }
            return result;
        }
        private void addIndexKeys() {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("ALTER TABLE `{0}` ADD INDEX(`{1}`,`{2}`);",
                        AppLibrary.db.tables.ProductGroups.TableName,
                        AppLibrary.db.tables.ProductGroups.ownerId,
                        AppLibrary.db.tables.ProductGroups.eventId);

                    dbCommand.CommandText += String.Format("ALTER TABLE `{0}` ADD INDEX(`{1}`);",
                        AppLibrary.db.tables.Products.TableName,
                        AppLibrary.db.tables.Products.groupId);


                    dbCommand.CommandText += String.Format("ALTER TABLE `{0}` ADD INDEX(`{1}`,`{2}`);",
                        AppLibrary.db.tables.Basket.TableName,
                        AppLibrary.db.tables.Basket.productId,
                        AppLibrary.db.tables.Basket.customerId);

                    dbCommand.CommandText += String.Format("ALTER TABLE `{0}` ADD INDEX(`{1}`);",
                        AppLibrary.db.tables.Orders.TableName,
                        AppLibrary.db.tables.Orders.customerId);

                    dbCommand.CommandText += String.Format("ALTER TABLE `{0}` ADD INDEX(`{1}`,`{2}`);",
                        AppLibrary.db.tables.OrderProducts.TableName,
                        AppLibrary.db.tables.OrderProducts.productId,
                        AppLibrary.db.tables.OrderProducts.orderId);

                    dbCommand.ExecuteNonQuery();
                }
            }
        }
        private void createTables()
        {
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString))
            {
                this.dbConnection.Open();

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` BIGINT," +
                        "`{3}` BIGINT," +
                        "`{4}` TEXT," +
                        "`{5}` REAL," +
                        "`{6}` REAL," +
                        "`{7}` REAL," +
                        "`{8}` REAL," +
                        "`{9}` BIT(1)" +
                        ");", AppLibrary.db.tables.ProductGroups.TableName,
                        AppLibrary.db.tables.ProductGroups.id,
                        AppLibrary.db.tables.ProductGroups.ownerId,
                        AppLibrary.db.tables.ProductGroups.eventId,
                        AppLibrary.db.tables.ProductGroups.createDate,
                        AppLibrary.db.tables.ProductGroups.priceTurkishLira,
                        AppLibrary.db.tables.ProductGroups.priceDollar,
                        AppLibrary.db.tables.ProductGroups.priceEuro,
                        AppLibrary.db.tables.ProductGroups.pricePound,
                        AppLibrary.db.tables.ProductGroups.isSliderActive);

                    dbCommand.ExecuteNonQuery();
                }

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` BIGINT," +
                        "`{3}` TEXT," +
                        "`{4}` TEXT," +
                        "`{5}` BIT(1)" +
                        ");", AppLibrary.db.tables.Products.TableName,
                        AppLibrary.db.tables.Products.id,
                        AppLibrary.db.tables.Products.groupId,
                        AppLibrary.db.tables.Products.image,
                        AppLibrary.db.tables.Products.imageHigh,
                        AppLibrary.db.tables.Products.isSliderActive);

                    dbCommand.ExecuteNonQuery();
                }

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` TEXT," +
                        "`{3}` TEXT," +
                        "`{4}` TEXT" +
                        ")", AppLibrary.db.tables.ProductOwner.TableName,
                        AppLibrary.db.tables.ProductOwner.id,
                        AppLibrary.db.tables.ProductOwner.image,
                        AppLibrary.db.tables.ProductOwner.name,
                        AppLibrary.db.tables.ProductOwner.createDate);
                    dbCommand.ExecuteNonQuery();
                }

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` TEXT," +
                        "`{3}` TEXT," +
                        "`{4}` TEXT" +
                        ")", AppLibrary.db.tables.Events.TableName,
                        AppLibrary.db.tables.Events.id,
                        AppLibrary.db.tables.Events.image,
                        AppLibrary.db.tables.Events.name,
                        AppLibrary.db.tables.Events.createDate);
                    dbCommand.ExecuteNonQuery();
                }

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` TEXT," +
                        "`{3}` TEXT," +
                        "`{4}` TEXT," +
                        "`{5}` TEXT," +
                        "`{6}` TEXT" +
                        ")", AppLibrary.db.tables.Customers.TableName,
                        AppLibrary.db.tables.Customers.id,
                        AppLibrary.db.tables.Customers.name,
                        AppLibrary.db.tables.Customers.email,
                        AppLibrary.db.tables.Customers.room,
                        AppLibrary.db.tables.Customers.password,
                        AppLibrary.db.tables.Customers.createDate);
                    dbCommand.ExecuteNonQuery();
                }

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` BIGINT," +
                        "`{3}` BIGINT," +
                        "`{4}` BIGINT," +
                        "`{5}` TEXT" +
                        ");", AppLibrary.db.tables.Basket.TableName,
                        AppLibrary.db.tables.Basket.id,
                        AppLibrary.db.tables.Basket.count,
                        AppLibrary.db.tables.Basket.productId,
                        AppLibrary.db.tables.Basket.customerId,
                        AppLibrary.db.tables.Basket.createDate);

                    dbCommand.ExecuteNonQuery();
                }

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` BIGINT," +
                        "`{3}` TEXT," +
                        "`{4}` BIT(1)," +
                        "`{5}` VARCHAR(20)" +
                        ");", AppLibrary.db.tables.Orders.TableName,
                        AppLibrary.db.tables.Orders.id,
                        AppLibrary.db.tables.Orders.customerId,
                        AppLibrary.db.tables.Orders.createDate,
                        AppLibrary.db.tables.Orders.isPaid,
                        AppLibrary.db.tables.Orders.paymentType);

                    dbCommand.ExecuteNonQuery();
                }

                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (" +
                        "`{1}` BIGINT PRIMARY KEY AUTO_INCREMENT," +
                        "`{2}` BIGINT," +
                        "`{3}` BIGINT," +
                        "`{4}` BIGINT," +
                        "`{5}` REAL," +
                        "`{6}` REAL," +
                        "`{7}` REAL," +
                        "`{8}` REAL" +
                        ");", AppLibrary.db.tables.OrderProducts.TableName,
                        AppLibrary.db.tables.OrderProducts.id,
                        AppLibrary.db.tables.OrderProducts.count,
                        AppLibrary.db.tables.OrderProducts.productId,
                        AppLibrary.db.tables.OrderProducts.orderId,
                        AppLibrary.db.tables.OrderProducts.priceTurkishLira,
                        AppLibrary.db.tables.OrderProducts.priceDollar,
                        AppLibrary.db.tables.OrderProducts.priceEuro,
                        AppLibrary.db.tables.OrderProducts.pricePound);

                    dbCommand.ExecuteNonQuery();
                }

                this.dbConnection.Close();
            }
        }
    }
}
