using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    public class Program
    {

        static void MainFromWeb(string[] args)
        {
            string connStr = ConfigurationManager.AppSettings["MongoServerSettings"];

            var client = new MongoClient(connStr);
            //var client = new MongoClient("mongodb://localhost:27017");
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");

            //创建Database数据库
            var database = client.GetDatabase("CSharpMongoTest");

            //创建Collection集合
            var collection = database.GetCollection<BsonDocument>("Collection1");

            //创建一条Bson数据
            BsonDocument document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            };

            //插入一条数据
            collection.InsertOne(document);

            //查询数据个数
            long count = collection.CountDocuments(document);
            Console.WriteLine(count);

            //查询数据
            BsonDocument document1 = collection.Find<BsonDocument>(document).FirstOrDefault();
            Console.WriteLine(document1.ToString());

            //更新数据 创建过滤器 和 更新数据器
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("name", "MongoDB");
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("name", "Ghazi");

            //将name为MongoDB更新为Ghazi
            collection.UpdateMany(filter, update);

            //删除数据
            var filterCount = Builders<BsonDocument>.Filter.Eq("count", 101);

            //删除多条count是101的数据
            collection.DeleteMany(filterCount);
            

            //添加多条
            BsonDocument document2 = new BsonDocument();
            document2.Add("name", "MongoDB");
            document2.Add("type", "Database");
            document2.Add("count", "1");

            collection.InsertOne(document2);

            Console.ReadKey();
        }

        static void printCustomer(Customer customer)
        {
            Console.WriteLine(
                customer.CusId + ", " + 
                customer.Name + ", " + 
                customer.Demo + ", " +
                customer.SubTime.ToString());
        }

        static void Main(string[] args)
        {
            // 获取连接字符串
            string connStr = ConfigurationManager.AppSettings["MongoServerSettings"];

            // 创建连接客户端的对象
            var client = new MongoClient(connStr);

            // 获取数据库，如果不存在则创建新的一个
            IMongoDatabase database = client.GetDatabase("TestDB");

            // 获取集合名字
            var collectionName = typeof(Customer).Name;

            // 获取集合，若不存在则创建一个新的
            var collection = database.GetCollection<Customer>(collectionName);

            #region 添加实体
            // InsertOne
            //for (int i = 0; i < 10; i++)
            //{
            //    Customer customer = new Customer();
            //    customer.CusId = i;
            //    customer.Name = "NameDemo" + i;
            //    customer.SubTime = DateTime.Now;
            //    customer.Demo = "Demo";
            //    customer.Orders = new List<Order>()
            //    {
            //        new Order() { OrderId = i, Content = i+DateTime.Now.ToString() },
            //        new Order() { OrderId = i, Content = i+DateTime.Now.ToString() }
            //    };

            //    collection.InsertOne(customer);
            //}

            //InsertMany
            //List<Customer> customers = new List<Customer>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Customer customer = new Customer();
            //    customer.CusId = i;
            //    customer.Name = "NameDemo" + i;
            //    customer.SubTime = DateTime.Now;
            //    customer.Demo = "InsertMany";
            //    customer.Orders = new List<Order>()
            //    {
            //        new Order() { OrderId = i, Content = i+DateTime.Now.ToString() },
            //        new Order() { OrderId = i, Content = i+DateTime.Now.ToString() }
            //    };
            //    customers.Add(customer);
            //}
            //collection.InsertMany(customers);

            #endregion

            #region 查找
            Console.WriteLine("#data that gt 5:");
            var filter = Builders<Customer>.Filter.Gt("CusId", 5);
            var cursor = collection.Find(filter).ToCursor();
            foreach (var customer in cursor.ToEnumerable())
            {
                printCustomer(customer);
            }

            Console.WriteLine("#data in the collection: " + collection.CountDocuments(u => true));

            Console.WriteLine("#data that gt 5 and lt 8:");
            var tmp = from c in collection.AsQueryable()
                      where c.CusId > 5 && c.CusId < 8
                      select c;
            foreach (var customer in tmp)
            {
                printCustomer(customer);
            }

            var customerList = collection.Find(c => true).ToList();
            Console.WriteLine("All data({0}):", customerList.Count());
            foreach (var customer in customerList)
            {
                printCustomer(customer);
            }
            #endregion

            #region 更新
            //var filer1 = Builders<Customer>.Filter.Eq(u => u.Demo, "InsertMany");
            //var update = Builders<Customer>.Update.Set(u => u.Name, "UpdateMany");
            //collection.UpdateMany(filer1, update);

            #endregion
            Console.ReadKey();
        }
    }
}
