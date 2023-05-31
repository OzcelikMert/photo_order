using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySqlConnector;

namespace Photo_Order.config.db.functions
{
    class Select {
        private MySqlConnection dbConnection { get; set; }

        public List<ListProductGroups> getProductGroups(long id = 0, string searchText = "") {
            var list = new List<ListProductGroups>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    string where =
                        (id > 0) ? String.Format("where {0} = @_1", AppLibrary.db.tables.ProductGroups.id) :
                        (!AppLibrary.Var.isNullOrEmpty(searchText)) ? String.Format("where {0} like @_2 or {1} like @_2", AppLibrary.db.tables.ProductGroups.createDate, AppLibrary.db.tables.ProductOwner.name) :
                        "";

                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.ProductGroups.TableName);
                    dbCommand.CommandText += String.Format("left join {0} on {1}={2} ", AppLibrary.db.tables.ProductOwner.TableName, AppLibrary.db.tables.ProductOwner.id, AppLibrary.db.tables.ProductGroups.ownerId);
                    dbCommand.CommandText += String.Format("left join {0} on {1}={2} ", AppLibrary.db.tables.Events.TableName, AppLibrary.db.tables.Events.id, AppLibrary.db.tables.ProductGroups.eventId);
                    dbCommand.CommandText += where;
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.ProductGroups.id);

                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", "%" + searchText + "%");

                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            string ownerId = AppLibrary.Var.isNullOrEmpty(reader[AppLibrary.db.tables.ProductOwner.id].ToString()) ? "0" : reader[AppLibrary.db.tables.ProductOwner.id].ToString();
                            string eventId = AppLibrary.Var.isNullOrEmpty(reader[AppLibrary.db.tables.Events.id].ToString()) ? "0" : reader[AppLibrary.db.tables.Events.id].ToString();
                            list.Add(new ListProductGroups {
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.ProductGroups.id].ToString()),
                                ownerName = reader[AppLibrary.db.tables.ProductOwner.name].ToString(),
                                ownerId = Convert.ToInt64(ownerId),
                                eventName = reader[AppLibrary.db.tables.Events.name].ToString(),
                                eventId = Convert.ToInt64(eventId),
                                createDate = reader[AppLibrary.db.tables.ProductGroups.createDate].ToString(),
                                priceTurkishLira = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceTurkishLira].ToString()),
                                priceDollar = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceDollar].ToString()),
                                priceEuro = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceEuro].ToString()),
                                pricePound = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.pricePound].ToString()),
                                isSliderActive = Convert.ToBoolean(Convert.ToInt32(reader[AppLibrary.db.tables.ProductGroups.isSliderActive].ToString()))
                            });
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return list;
        }
        public List<ListProduct> getProducts(long id = 0, long groupId = 0, string searchText = "") {
            var list = new List<ListProduct>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    string whereId = (id > 0) ? String.Format("{0} = @_1", AppLibrary.db.tables.Products.id) : "",
                        whereGroupId = (groupId > 0) ? String.Format("{0} = @_2", AppLibrary.db.tables.Products.groupId) : "",
                        whereSearchText = (!AppLibrary.Var.isNullOrEmpty(searchText)) ? String.Format("({0} like @_3 or {1} like @_3)", AppLibrary.db.tables.Products.image, AppLibrary.db.tables.ProductOwner.name) : "";

                    string where = String.Format("{0}{1}{2}",
                            whereId.Length > 0 ? String.Format("and {0} ", whereId) : "",
                            whereGroupId.Length > 0 ? String.Format("and {0} ", whereGroupId) : "",
                            whereSearchText.Length > 0 ? String.Format("and {0} ", whereSearchText) : ""
                        );
                    where = where.Length > 0 ? String.Format(" where {0} ", where.Substring(3)) : "";

                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Products.TableName);
                    dbCommand.CommandText += String.Format("left join {0} on {1}={2} ", AppLibrary.db.tables.ProductGroups.TableName, AppLibrary.db.tables.ProductGroups.id, AppLibrary.db.tables.Products.groupId);
                    dbCommand.CommandText += String.Format("left join {0} on {1}={2} ", AppLibrary.db.tables.ProductOwner.TableName, AppLibrary.db.tables.ProductOwner.id, AppLibrary.db.tables.ProductGroups.ownerId);
                    dbCommand.CommandText += String.Format("left join {0} on {1}={2} ", AppLibrary.db.tables.Events.TableName, AppLibrary.db.tables.Events.id, AppLibrary.db.tables.ProductGroups.eventId);
                    dbCommand.CommandText += where;
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Products.id);

                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", groupId);
                    dbCommand.Parameters.AddWithValue("@_3", "%" + searchText + "%");

                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            string ownerId = AppLibrary.Var.isNullOrEmpty(reader[AppLibrary.db.tables.ProductOwner.id].ToString()) ? "0" : reader[AppLibrary.db.tables.ProductOwner.id].ToString();
                            string eventId = AppLibrary.Var.isNullOrEmpty(reader[AppLibrary.db.tables.Events.id].ToString()) ? "0" : reader[AppLibrary.db.tables.Events.id].ToString();
                            list.Add(new ListProduct { 
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.Products.id].ToString()),
                                groupId = Convert.ToInt64(reader[AppLibrary.db.tables.Products.groupId].ToString()),
                                image = reader[AppLibrary.db.tables.Products.image].ToString(),
                                imageHigh = reader[AppLibrary.db.tables.Products.imageHigh].ToString(),
                                ownerName = reader[AppLibrary.db.tables.ProductOwner.name].ToString(),
                                ownerId = Convert.ToInt64(ownerId),
                                eventName = reader[AppLibrary.db.tables.Events.name].ToString(),
                                eventId = Convert.ToInt64(eventId),
                                createDate = reader[AppLibrary.db.tables.ProductGroups.createDate].ToString(),
                                priceTurkishLira = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceTurkishLira].ToString()),
                                priceDollar = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceDollar].ToString()),
                                priceEuro = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceEuro].ToString()),
                                pricePound = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.pricePound].ToString()),
                                isSliderActive = Convert.ToBoolean(Convert.ToInt32(reader[AppLibrary.db.tables.Products.isSliderActive].ToString()))
                            });
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return list;
        }
        public List<ListProductOwner> getProductOwners(long id = 0, string name = "") {
            var list = new List<ListProductOwner>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    string where = 
                        (id > 0) ? String.Format("where {0} = @_1", AppLibrary.db.tables.ProductOwner.id) :
                        (!AppLibrary.Var.isNullOrEmpty(name)) ? String.Format("where {0} like @_2", AppLibrary.db.tables.ProductOwner.name) : 
                        "";

                    dbCommand.CommandText = String.Format("select * from {0} {1}", AppLibrary.db.tables.ProductOwner.TableName, where);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.ProductOwner.id);

                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", "%" + name + "%");

                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            list.Add(new ListProductOwner {
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.ProductOwner.id].ToString()),
                                image = reader[AppLibrary.db.tables.ProductOwner.image].ToString(),
                                createDate = reader[AppLibrary.db.tables.ProductOwner.createDate].ToString(),
                                name = reader[AppLibrary.db.tables.ProductOwner.name].ToString(),
                            });
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return list;
        }
        public List<ListEvents> getEvents(long id = 0, string name = "") {
            var list = new List<ListEvents>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    string where =
                        (id > 0) ? String.Format("where {0} = @_1", AppLibrary.db.tables.Events.id) :
                        (!AppLibrary.Var.isNullOrEmpty(name)) ? String.Format("where {0} like @_2", AppLibrary.db.tables.Events.name) :
                        "";

                    dbCommand.CommandText = String.Format("select * from {0} {1}", AppLibrary.db.tables.Events.TableName, where);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Events.id);

                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", "%" + name + "%");

                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            list.Add(new ListEvents {
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.Events.id].ToString()),
                                image = reader[AppLibrary.db.tables.Events.image].ToString(),
                                createDate = reader[AppLibrary.db.tables.Events.createDate].ToString(),
                                name = reader[AppLibrary.db.tables.Events.name].ToString(),
                            });
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return list;
        }
        public List<ListCustomers> getCustomers(long id = 0, string name = "") {
            var listCustomers = new List<ListCustomers>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    string where =
                        (id > 0) ? String.Format("where {0} = @_1", AppLibrary.db.tables.Customers.id) :
                        (!AppLibrary.Var.isNullOrEmpty(name)) ? String.Format("where {0} like @_2 or {1} like @_3", AppLibrary.db.tables.Customers.name, AppLibrary.db.tables.Customers.room) :
                        "";

                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Customers.TableName);
                    dbCommand.CommandText += where;
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Customers.id);

                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", "%" + name + "%");
                    dbCommand.Parameters.AddWithValue("@_3", "%" + name + "%");

                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            listCustomers.Add(new ListCustomers {
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.Customers.id].ToString()),
                                room = reader[AppLibrary.db.tables.Customers.room].ToString(),
                                name = reader[AppLibrary.db.tables.Customers.name].ToString(),
                                email = reader[AppLibrary.db.tables.Customers.email].ToString(),
                                createDate = reader[AppLibrary.db.tables.Customers.createDate].ToString()
                            });
                            
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return listCustomers;
        }

        public List<ListOrder> getOrders(string customerName = "", bool isPaid = false) {
            var list = new List<ListOrder>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    string where = (!AppLibrary.Var.isNullOrEmpty(customerName)) ? String.Format("and ({0} like @_1 or {1} like @_2)", AppLibrary.db.tables.Customers.name, AppLibrary.db.tables.Customers.room) : "";

                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Orders.TableName);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.Customers.TableName, AppLibrary.db.tables.Customers.id, AppLibrary.db.tables.Orders.customerId);
                    dbCommand.CommandText += String.Format("where {0}={1} {2}", AppLibrary.db.tables.Orders.isPaid, isPaid, where);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Orders.id);

                    dbCommand.Parameters.AddWithValue("@_1", "%" + customerName + "%");
                    dbCommand.Parameters.AddWithValue("@_2", "%" + customerName + "%");

                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            list.Add(new ListOrder {
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.Orders.id].ToString()),
                                customerId = Convert.ToInt64(reader[AppLibrary.db.tables.Customers.id].ToString()),
                                customerName = reader[AppLibrary.db.tables.Customers.name].ToString(),
                                customerRoom = reader[AppLibrary.db.tables.Customers.room].ToString(),
                                customerEmail = reader[AppLibrary.db.tables.Customers.email].ToString(),
                                createDate = reader[AppLibrary.db.tables.Orders.createDate].ToString(),
                                paymentType = reader[AppLibrary.db.tables.Orders.paymentType].ToString(),
                            });
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return list;
        }

        public List<ListOrderProduct> getOrderProduct(long id = 0, long orderId = 0) {
            var list = new List<ListOrderProduct>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.OrderProducts.TableName);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.Orders.TableName, AppLibrary.db.tables.Orders.id, AppLibrary.db.tables.OrderProducts.orderId);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.Products.TableName, AppLibrary.db.tables.Products.id, AppLibrary.db.tables.OrderProducts.productId);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.ProductGroups.TableName, AppLibrary.db.tables.ProductGroups.id, AppLibrary.db.tables.Products.groupId);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.ProductOwner.TableName, AppLibrary.db.tables.ProductOwner.id, AppLibrary.db.tables.ProductGroups.ownerId);
                    dbCommand.CommandText += String.Format("where {0}=@_1 ", (id > 0) ? AppLibrary.db.tables.OrderProducts.id : AppLibrary.db.tables.OrderProducts.orderId);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.OrderProducts.id);
                    dbCommand.Parameters.AddWithValue("@_1", (id > 0) ? id : orderId);
                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            list.Add(new ListOrderProduct {
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.OrderProducts.id].ToString()),
                                count = Convert.ToInt32(reader[AppLibrary.db.tables.OrderProducts.count].ToString()),
                                productId = Convert.ToInt64(reader[AppLibrary.db.tables.Products.id].ToString()),
                                productGroupId = Convert.ToInt64(reader[AppLibrary.db.tables.Products.groupId].ToString()),
                                productImage = reader[AppLibrary.db.tables.Products.image].ToString(),
                                productImageHigh = reader[AppLibrary.db.tables.Products.imageHigh].ToString(),
                                productOwnerName = reader[AppLibrary.db.tables.ProductOwner.name].ToString(),
                                productOwnerId = Convert.ToInt64(reader[AppLibrary.db.tables.ProductOwner.id].ToString()),
                                createDate = reader[AppLibrary.db.tables.Orders.createDate].ToString(),
                                priceTurkishLira = Convert.ToDouble(reader[AppLibrary.db.tables.OrderProducts.priceTurkishLira].ToString()),
                                priceDollar = Convert.ToDouble(reader[AppLibrary.db.tables.OrderProducts.priceDollar].ToString()),
                                priceEuro = Convert.ToDouble(reader[AppLibrary.db.tables.OrderProducts.priceEuro].ToString()),
                                pricePound = Convert.ToDouble(reader[AppLibrary.db.tables.OrderProducts.pricePound].ToString())
                            });
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return list;
        }

        public int getBasketCount(long customerId) {
            int count = 0;
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Basket.TableName);
                    dbCommand.CommandText += String.Format("where {0}=@_1 ", AppLibrary.db.tables.Basket.customerId);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Basket.id);
                    dbCommand.Parameters.AddWithValue("@_1", customerId);
                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            count++;
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return count;
        }
        public int getOrderCount(long customerId) {
            int count = 0;
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString))
            {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection))
                {
                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Orders.TableName);
                    dbCommand.CommandText += String.Format("where {0}=@_1 ", AppLibrary.db.tables.Orders.customerId);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Orders.id);
                    dbCommand.Parameters.AddWithValue("@_1", customerId);
                    using (MySqlDataReader reader = dbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count++;
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return count;
        }

        public class ListProductGroups {
            public long id { get; set; }
            public long ownerId { get; set; }
            public long eventId { get; set; }
            public string ownerName { get; set; }
            public string eventName { get; set; }
            public string createDate { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
            public bool isSliderActive { get; set; }
        }
        public class ListProduct {
            public long id { get; set; }
            public long groupId { get; set; }
            public string image { get; set; }
            public string imageHigh { get; set; }
            public string ownerName { get; set; }
            public long ownerId { get; set; }
            public string eventName { get; set; }
            public long eventId { get; set; }
            public string createDate { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
            public bool isSliderActive { get; set; }
        }
        public class ListProductOwner {
            public long id { get; set; }
            public string image { get; set; }
            public string createDate { get; set; }
            public string name { get; set; }
        }
        public class ListEvents {
            public long id { get; set; }
            public string image { get; set; }
            public string createDate { get; set; }
            public string name { get; set; }
        }
        public class ListCustomers {
            public long id { get; set; }
            public string room { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string createDate { get; set; }
        }
        public class ListOrder {
            public long id { get; set; }
            public string createDate { get; set; }
            public string paymentType { get; set; }
            public long customerId { get; set; }
            public string customerName { get; set; }
            public string customerEmail { get; set; }
            public string customerRoom { get; set; }
        }
        public class ListOrderProduct　{
            public long id { get; set; }
            public int count { get; set; }
            public long productId { get; set; }
            public long productGroupId { get; set; }
            public string productImage { get; set; }
            public string productImageHigh { get; set; }
            public long productOwnerId { get; set; }
            public string productOwnerName { get; set; }
            public string createDate { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
        }
    }
}
