namespace CqrsContacts.Domain.Contacts.Handlers
{
    using CqrsContacts.Domain.Common.Interfaces;
    using CqrsContacts.Domain.Contacts.Commands.Requests;
    using CqrsContacts.Domain.Contacts.Commands.Responses;
    using CqrsContacts.Domain.Contacts.Models;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateContactHandler : IRequestHandler<CreateContactRequest, CreateContactResponse>
    {
        private readonly IMongoRepository<Contact> _mongoRepository;
        public CreateContactHandler(IMongoRepository<Contact> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<CreateContactResponse> Handle(CreateContactRequest request, CancellationToken cancellationToken)
        {
            var contact = new Contact
            {
                Name = request.Name,
                Phone = request.Phone,
            };

            await _mongoRepository.InsertOneAsync(contact);

            return new CreateContactResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Phone = contact.Phone,
            };
        }
    }
}
