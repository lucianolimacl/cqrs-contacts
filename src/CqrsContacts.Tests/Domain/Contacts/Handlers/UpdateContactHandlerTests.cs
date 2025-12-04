using CqrsContacts.Domain.Common.Interfaces;
using CqrsContacts.Domain.Contacts.Commands.Requests;
using CqrsContacts.Domain.Contacts.Handlers;
using CqrsContacts.Domain.Contacts.Models;
using FluentValidation;
using MongoDB.Driver;
using Moq;
using System.Linq.Expressions;

namespace CqrsContacts.Tests.Domain.Contacts.Handlers;

public class UpdateContactHandlerTests
{
    [Fact(DisplayName = "update a existing contact")]
    public async void Handle_WithExistingContact_ShouldUpdateContact()
    {
        //Arrange
        var mongoRepository = new Mock<IMongoRepository<Contact>>();
        var updateContactHandler = new UpdateContactHandler(mongoRepository.Object);
        var contact = new Contact
        {
            Id = "Id",
            Name = "Name",
            Phone = "00-0000"
        };

        var request = new UpdateContactRequest
        {
            Id = "Id",
            Name = "Name2",
            Phone = "11-1111"
        };

        mongoRepository
            .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Contact, bool>>>(), It.IsAny<FindOptions<Contact, Contact>>()))
            .Returns(Task.FromResult(contact));

        //Act
        var result = await updateContactHandler.Handle(request, CancellationToken.None);

        //Assert    
        Assert.NotNull(result.Name);
        Assert.NotNull(result.Phone);
        mongoRepository.Verify(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Contact, bool>>>(), It.Is<Contact>(x => x.Id == contact.Id)), Times.Once);
        mongoRepository.Verify(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Contact, bool>>>(), It.Is<Contact>(x => x.Name == request.Name)), Times.Once);
    }

    [Fact(DisplayName = "update a non existing contact")]
    public void Handle_WithNonExistingContact_ShouldThrowException()
    {
        //Arrange
        var mongoRepository = new Mock<IMongoRepository<Contact>>();
        var updateContactHandler = new UpdateContactHandler(mongoRepository.Object);

        var request = new UpdateContactRequest
        {
            Id = "Id",
            Name = "Name2",
            Phone = "11-1111"
        };

        //Act
        var result = Assert.ThrowsAsync<ValidationException>(() => updateContactHandler.Handle(request, CancellationToken.None));

        //Assert            
        Assert.Equal("contact not found", result.Result.Message);
        mongoRepository.Verify(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Contact, bool>>>(), It.IsAny<Contact>()), Times.Never);
    }
}
