namespace CqrsContacts.Tests.Domain.Common.Repositories;

using CqrsContacts.Domain.Common.Options;
using CqrsContacts.Domain.Common.Repositories;
using CqrsContacts.Domain.Contacts.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

public class MongoRepositoryTests
{
    [Fact(DisplayName = "insert a new contact")]
    public async Task InsertOneAsync_WhenInsertContact_ShouldSaveTheNewContact()
    {
        //Arrange
        var mongoClient = new Mock<IMongoClient>();
        var mongoDatabase = new Mock<IMongoDatabase>();
        var mongoCollection = new Mock<IMongoCollection<Contact>>();
        var contact = new Contact { Name = "Name", Phone = "00-0000" };

        mongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), null)).Returns(mongoDatabase.Object);
        mongoDatabase.Setup(x => x.GetCollection<Contact>(It.IsAny<string>(), null)).Returns(mongoCollection.Object);

        var mongoRepository = new MongoRepository<Contact>(Options.Create(new ContactDatabaseOptions()), mongoClient.Object);

        //Act
        await mongoRepository.InsertOneAsync(contact);

        //Assert
        mongoCollection.Verify((x) => x.InsertOneAsync(It.Is<Contact>(x => x.Name == contact.Name), null, CancellationToken.None), Times.Once);
        mongoCollection.Verify((x) => x.InsertOneAsync(It.Is<Contact>(x => x.Phone == contact.Phone), null, CancellationToken.None), Times.Once);
    }
}
