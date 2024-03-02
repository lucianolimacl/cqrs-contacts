namespace CqrsContacts.Tests.Domain.Contacts.Handlers
{
    using CqrsContacts.Domain.Common.Interfaces;
    using CqrsContacts.Domain.Contacts.Commands.Requests;
    using CqrsContacts.Domain.Contacts.Handlers;
    using CqrsContacts.Domain.Contacts.Models;
    using Moq;

    public class CreateContactHandlerTest
    {
        [Fact(DisplayName = "create contact handler")]
        public async void HandleTest()
        {
            var mongoRepository = new Mock<IMongoRepository<Contact>>();
            var createContactHandler = new CreateContactHandler(mongoRepository.Object);

            var result = await createContactHandler.Handle(new CreateContactRequest
            {
                Name = "Test",
                Phone = "001"
            }, CancellationToken.None);

            Assert.NotNull(result.Name);
            Assert.NotNull(result.Phone);

            mongoRepository.Verify(x => x.InsertOneAsync(It.IsAny<Contact>()), Times.Once);
        }
    }
}
