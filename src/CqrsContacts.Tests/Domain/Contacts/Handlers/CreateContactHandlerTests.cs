namespace CqrsContacts.Tests.Domain.Contacts.Handlers;

using CqrsContacts.Domain.Common.Interfaces;
using CqrsContacts.Domain.Contacts.Commands.Requests;
using CqrsContacts.Domain.Contacts.Handlers;
using CqrsContacts.Domain.Contacts.Models;
using Moq;

public class CreateContactHandlerTests
{
    [Fact(DisplayName = "create a valid contact")]
    public async void Handle_WithValidContact_ShouldSaveContact()
    {
        //Arrange
        var mongoRepository = new Mock<IMongoRepository<Contact>>();
        var createContactHandler = new CreateContactHandler(mongoRepository.Object);
        var request = new CreateContactRequest
        {
            Name = "Name",
            Phone = "00-0000"
        };

        //Act
        var result = await createContactHandler.Handle(request, CancellationToken.None);

        //Assert    
        Assert.NotNull(result.Name);
        Assert.NotNull(result.Phone);
        mongoRepository.Verify(x => x.InsertOneAsync(It.Is<Contact>(x => x.Name == request.Name)), Times.Once);
    }
}
