using Catalog.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
   

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        
       private const string databaseName = "catalog";
       private const string collectionName = "items";
       private readonly IMongoCollection<Item> itemsCollection;

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
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}