using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();

            var document = new BsonDocument
            {
                {"CustomerName", customer.CustomerName },
                {"CustomerSurname", customer.CustomerSurname },
                {"CustomerCity", customer.CustomerCity },
                {"CustomerBalance", customer.CustomerBalance },
                {"CustomerShoppingCount", customer.CustomerShoppingCount },
            };

            customerCollection.InsertOne(document);
        }

        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = customer["_id"].ToString(),
                    CustomerName = customer["CustomerName"].ToString(),
                    CustomerSurname = customer["CustomerSurname"].ToString(),
                    CustomerCity = customer["CustomerCity"].ToString(),
                    CustomerBalance = decimal.Parse(customer["CustomerBalance"].ToString()),
                    CustomerShoppingCount = int.Parse(customer["CustomerShoppingCount"].ToString())
                });
            }
            return customerList;
        }

        public void DeleteCustomer(string customerId)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();

            var deleted = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customerId));
            customerCollection.DeleteOne(deleted);
        }

        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);
            customerCollection.UpdateOne(filter, updatedValue);
        }

        public Customer GetCustomer(string customerId, string customerName, string customerSurname)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();

            // Filtreyi oluştur
            var filter = Builders<BsonDocument>.Filter.Empty;
            if (!string.IsNullOrEmpty(customerId))
            {
                filter &= Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customerId));
            }
            if (!string.IsNullOrEmpty(customerName))
            {
                filter &= Builders<BsonDocument>.Filter.Eq("CustomerName", customerName);
            }
            if (!string.IsNullOrEmpty(customerSurname))
            {
                filter &= Builders<BsonDocument>.Filter.Eq("CustomerSurname", customerSurname);
            }
            // İlk sonucu getir
            var customerDoc = customerCollection.Find(filter).FirstOrDefault();

            if (customerDoc == null)
                return null;

            // Customer objesine dönüştür
            return new Customer
            {
                CustomerId = customerDoc["_id"].ToString(),
                CustomerName = customerDoc["CustomerName"].ToString(),
                CustomerSurname = customerDoc.Contains("CustomerSurname") ? customerDoc["CustomerSurname"].ToString() : "Unknown",
                CustomerCity = customerDoc["CustomerCity"].ToString(),
                CustomerBalance = decimal.Parse(customerDoc["CustomerBalance"].ToString()),
                CustomerShoppingCount = int.Parse(customerDoc["CustomerShoppingCount"].ToString())
            };
        }

    }
}
