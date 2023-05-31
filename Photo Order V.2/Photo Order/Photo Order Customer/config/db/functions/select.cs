using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySqlConnector;

namespace Photo_Order_Customer.config.db.functions
{
    public class Select {
        private MySqlConnection dbConnection { get; set; }
        public List<ListProduct> getProducts(long id = 0, List<long> productOwners = null, List<long> events = null, string[] date = null, string orderBy = "", int limit = 0, int isSliderActive = 2) {
            var list = new List<ListProduct>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    // Where
                    string whereId = (id > 0) ? String.Format(" {0} = @_1 ", AppLibrary.db.tables.Products.id) : "",
                        whereIsSliderActive = (isSliderActive == 1 || isSliderActive == 0) ? String.Format(" {0} = @_3 and {1} = @_3 ", AppLibrary.db.tables.Products.isSliderActive, AppLibrary.db.tables.ProductGroups.isSliderActive) : "",
                        whereProductOwner = "",
                        whereEvent = "",
                        whereDate = "";
                    // Product Owner Loop
                    if (productOwners != null && productOwners.Count > 0) {
                        whereProductOwner = String.Format(" {0} IN (", AppLibrary.db.tables.ProductGroups.ownerId);
                        foreach (var productOwner in productOwners) {
                            whereProductOwner += productOwner.ToString() + ",";
                        }
                        whereProductOwner = whereProductOwner.Substring(0, whereProductOwner.Length - 1);
                        whereProductOwner += ") ";
                    }
                    // Loop End
                    // Events Loop
                    if (events != null && events.Count > 0)
                    {
                        whereEvent = String.Format(" {0} IN (", AppLibrary.db.tables.ProductGroups.eventId);
                        foreach (var _event in events)
                        {
                            whereEvent += _event.ToString() + ",";
                        }
                        whereEvent = whereEvent.Substring(0, whereEvent.Length - 1);
                        whereEvent += ") ";
                    }
                    // Loop
                    // Date Loop
                    if (date != null) {
                        whereDate = String.Format(" {0} IN (", AppLibrary.db.tables.ProductGroups.createDate);
                        foreach (var _date in date) {
                            whereDate += "'" + _date + "'" + ",";
                        }
                        whereDate = whereDate.Substring(0, whereDate.Length - 1);
                        whereDate += ") ";
                    }
                    // Loop End
                    string where = String.Format("{0}{1}{2}{3}{4}", 
                            whereId.Length > 0 ? String.Format(" and {0}", whereId) : "",
                            whereIsSliderActive.Length > 0 ? String.Format(" and {0}", whereIsSliderActive) : "",
                            whereProductOwner.Length > 0 ? String.Format(" and {0}", whereProductOwner) : "",
                            whereEvent.Length > 0 ? String.Format(" and {0}", whereEvent) : "",
                            whereDate.Length > 0 ? String.Format(" and {0}", whereDate) : ""
                        );

                    // Where End
                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Products.TableName);
                    dbCommand.CommandText += String.Format(" inner join {0} on {1}={2} ", AppLibrary.db.tables.ProductGroups.TableName, AppLibrary.db.tables.ProductGroups.id, AppLibrary.db.tables.Products.groupId);
                    dbCommand.CommandText += String.Format(" inner join {0} on {1}={2} ", AppLibrary.db.tables.ProductOwner.TableName, AppLibrary.db.tables.ProductOwner.id, AppLibrary.db.tables.ProductGroups.ownerId);
                    dbCommand.CommandText += where;
                    dbCommand.CommandText += (orderBy.Length > 0) ? String.Format(" order by {0} {1} ", AppLibrary.db.tables.Products.imageHigh, orderBy) : "";
                    dbCommand.CommandText += (limit > 0) ? String.Format(" limit {0} ", limit) : ""; ;
                    
                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", date);
                    dbCommand.Parameters.AddWithValue("@_3", isSliderActive);
                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            list.Add(new ListProduct { 
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.Products.id].ToString()),
                                groupId = Convert.ToInt64(reader[AppLibrary.db.tables.Products.groupId].ToString()),
                                image = reader[AppLibrary.db.tables.Products.image].ToString(),
                                imageHigh = reader[AppLibrary.db.tables.Products.imageHigh].ToString(),
                                createDate = reader[AppLibrary.db.tables.ProductGroups.createDate].ToString(),
                                ownerName = reader[AppLibrary.db.tables.ProductOwner.name].ToString(),
                                ownerId = Convert.ToInt64(reader[AppLibrary.db.tables.ProductGroups.ownerId].ToString()),
                                priceTurkishLira = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceTurkishLira].ToString()),
                                priceDollar = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceDollar].ToString()),
                                priceEuro = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceEuro].ToString()),
                                pricePound = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.pricePound].ToString()),
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
        public List<ListEvents> getEvents(long id = 0, string name = "")
        {
            var list = new List<ListEvents>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString))
            {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection))
                {
                    string where =
                        (id > 0) ? String.Format("where {0} = @_1", AppLibrary.db.tables.Events.id) :
                        (!AppLibrary.Var.isNullOrEmpty(name)) ? String.Format("where {0} like @_2", AppLibrary.db.tables.Events.name) :
                        "";

                    dbCommand.CommandText = String.Format("select * from {0} {1}", AppLibrary.db.tables.Events.TableName, where);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Events.id);

                    dbCommand.Parameters.AddWithValue("@_1", id);
                    dbCommand.Parameters.AddWithValue("@_2", "%" + name + "%");

                    using (MySqlDataReader reader = dbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ListEvents
                            {
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
        public long getCustomer(string room = "", string password = null, long customerId = 0) {
            long id = 0;
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Customers.TableName);
                    dbCommand.CommandText += String.Format("where {0}=@_1 ", (customerId > 0) ? AppLibrary.db.tables.Customers.id : AppLibrary.db.tables.Customers.room);
                    dbCommand.CommandText += (password != null) ? String.Format("and {0}=@_2", AppLibrary.db.tables.Customers.password) : "";
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Customers.id);

                    dbCommand.Parameters.AddWithValue("@_1", (customerId > 0) ? customerId.ToString() : room);
                    dbCommand.Parameters.AddWithValue("@_2", password);
                    
                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            id = Convert.ToInt64(reader[AppLibrary.db.tables.Customers.id].ToString());
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return id;
        }
        public List<ListOrder> getOrders(long customerId = 0, bool isPaid = false) {
            var list = new List<ListOrder>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString))
            {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection))
                {
                    string where = (!AppLibrary.Var.isNullOrEmpty(customerId)) ? String.Format("and {0} = @_1", AppLibrary.db.tables.Customers.id) : "";

                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Orders.TableName);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.Customers.TableName, AppLibrary.db.tables.Customers.id, AppLibrary.db.tables.Orders.customerId);
                    dbCommand.CommandText += String.Format("where {0}={1} {2}", AppLibrary.db.tables.Orders.isPaid, isPaid, where);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Orders.id);

                    dbCommand.Parameters.AddWithValue("@_1", customerId);

                    using (MySqlDataReader reader = dbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ListOrder
                            {
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
        public List<ListBasket> getBasket(long customerId) {
            var list = new List<ListBasket>();
            using (this.dbConnection = new MySqlConnection(AppLibrary.Values.dbInfo.connectionString)) {
                this.dbConnection.Open();
                using (MySqlCommand dbCommand = new MySqlCommand(null, this.dbConnection)) {
                    dbCommand.CommandText = String.Format("select * from {0} ", AppLibrary.db.tables.Basket.TableName);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.Products.TableName, AppLibrary.db.tables.Products.id, AppLibrary.db.tables.Basket.productId);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.ProductGroups.TableName, AppLibrary.db.tables.ProductGroups.id, AppLibrary.db.tables.Products.groupId);
                    dbCommand.CommandText += String.Format("inner join {0} on {1}={2} ", AppLibrary.db.tables.ProductOwner.TableName, AppLibrary.db.tables.ProductOwner.id, AppLibrary.db.tables.ProductGroups.ownerId);
                    dbCommand.CommandText += String.Format("where {0}=@_1 ", AppLibrary.db.tables.Basket.customerId);
                    dbCommand.CommandText += String.Format(" order by {0} desc", AppLibrary.db.tables.Basket.id);
                    dbCommand.Parameters.AddWithValue("@_1", customerId);
                    using (MySqlDataReader reader = dbCommand.ExecuteReader()) {
                        while (reader.Read()) {
                            list.Add(new ListBasket {
                                id = Convert.ToInt64(reader[AppLibrary.db.tables.Basket.id].ToString()),
                                productGroupId = Convert.ToInt64(reader[AppLibrary.db.tables.Products.groupId].ToString()),
                                count = Convert.ToInt32(reader[AppLibrary.db.tables.Basket.count].ToString()),
                                productId = Convert.ToInt64(reader[AppLibrary.db.tables.Products.id].ToString()),
                                productImage = reader[AppLibrary.db.tables.Products.image].ToString(),
                                productImageHigh = reader[AppLibrary.db.tables.Products.imageHigh].ToString(),
                                productOwnerName = reader[AppLibrary.db.tables.ProductOwner.name].ToString(),
                                productOwnerId = Convert.ToInt64(reader[AppLibrary.db.tables.ProductOwner.id].ToString()),
                                createDate = reader[AppLibrary.db.tables.Basket.createDate].ToString(),
                                priceTurkishLira = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceTurkishLira].ToString()),
                                priceDollar = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceDollar].ToString()),
                                priceEuro = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.priceEuro].ToString()),
                                pricePound = Convert.ToDouble(reader[AppLibrary.db.tables.ProductGroups.pricePound].ToString())
                            });
                        }
                    }
                }
                this.dbConnection.Close();
            }
            return list;
        }

        public class ListProduct {
            public long id { get; set; }
            public long groupId { get; set; }
            public long ownerId { get; set; }
            public string image { get; set; }
            public string imageHigh { get; set; }
            public string createDate { get; set; }
            public string ownerName { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
        }
        public class ListProductOwner {
            public long id { get; set; }
            public string image { get; set; }
            public string createDate { get; set; }
            public string name { get; set; }
        }
        public class ListEvents
        {
            public long id { get; set; }
            public string image { get; set; }
            public string createDate { get; set; }
            public string name { get; set; }
        }
        public class ListOrder
        {
            public long id { get; set; }
            public string createDate { get; set; }
            public string paymentType { get; set; }
            public long customerId { get; set; }
            public string customerName { get; set; }
            public string customerEmail { get; set; }
            public string customerRoom { get; set; }
        }
        public class ListBasket {
            public long id { get; set; }
            public long productGroupId { get; set; }
            public int count { get; set; }
            public long productId { get; set; }
            public string productImage { get; set; }
            public string productImageHigh { get; set; }
            public string productOwnerName { get; set; }
            public long productOwnerId { get; set; }
            public string createDate { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
        }
    }
}
