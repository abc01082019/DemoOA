using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    [BsonIgnoreExtraElements]
    public class Customer
    {
        public string Name { get; set; }
        public int CusId { get; set; }
        public DateTime SubTime { get; set; }
        public string Demo { get; set; }
        public List<Order> Orders { get; set; }

    }

    [BsonIgnoreExtraElements]
    public class Order
    {
        public int OrderId { get; set; }
        public string Content { get; set; }
    }
}
