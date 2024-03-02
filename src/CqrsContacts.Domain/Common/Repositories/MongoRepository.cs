﻿namespace CqrsContacts.Domain.Common.Repositories
{
    using CqrsContacts.Domain.Common.Interfaces;
    using CqrsContacts.Domain.Common.Options;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    public class MongoRepository<T> : IMongoRepository<T>
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IOptions<ContactDatabaseOptions> options, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(options.Value.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            return await IMongoCollectionExtensions.DeleteOneAsync(_collection,filter);
        }

        public async Task<IAsyncCursor<T>> FindAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null!)
        {
            return await IMongoCollectionExtensions.FindAsync<T>(_collection, filter, options);
        }
      
        public async Task InsertOneAsync(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T document)
        {
            return await IMongoCollectionExtensions.ReplaceOneAsync<T>(_collection, filter, document);

        }
    }
}
