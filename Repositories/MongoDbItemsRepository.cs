using Catalog.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
   

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        
       private const string databaseName = "catalog";
       private const string collectionName = "items";
       private readonly IMongoCollection<Item> itemsCollection;

       private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

       //() recieve a MongoDb Client, we need a new package "dotnet add package MongoDB.Driver
        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);
        }    

        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingitem => existingitem.Id, item.Id);
            itemsCollection.ReplaceOne(filter,item);
        }
    }
}