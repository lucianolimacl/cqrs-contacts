namespace CqrsContacts.Tests.Domain.Common.Repositories
{
    using CqrsContacts.Domain.Common.Options;
    using CqrsContacts.Domain.Common.Repositories;
    using CqrsContacts.Domain.Contacts.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Moq;

    public class MongoRepositoryTest
    {
        [Fact(DisplayName = "insert new contact")]
        public async Task InsertOneAsync_Test()
        {
            var mongoClient = new Mock<IMongoClient>();
            var mongoDatabase = new Mock<IMongoDatabase>();
            var mongoCollection = new Mock<IMongoCollection<Contact>>();

            mongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), null)).Returns(mongoDatabase.Object);
            mongoDatabase.Setup(x => x.GetCollection<Contact>(It.IsAny<string>(), null)).Returns(mongoCollection.Object);

            var mongoRepository = new MongoRepository<Contact>(Options.Create(new ContactDatabaseOptions()), mongoClient.Object);
            await mongoRepository.InsertOneAsync(new Contact { Name = "Test", Phone = "55-0000" });

            mongoCollection.Verify((x) => x.InsertOneAsync(It.IsAny<Contact>(), null, CancellationToken.None), Times.Once);
        }
    }
}
