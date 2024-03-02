namespace CqrsContacts.Domain.Contacts.Handlers
{
    using CqrsContacts.Domain.Common.Interfaces;
    using CqrsContacts.Domain.Contacts.Commands.Requests;
    using CqrsContacts.Domain.Contacts.Commands.Responses;
    using CqrsContacts.Domain.Contacts.Models;
    using FluentValidation;
    using MediatR;
    using MongoDB.Driver;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateContactHandler : IRequestHandler<UpdateContactRequest, UpdateContactResponse>
    {
        private readonly IMongoRepository<Contact> _mongoRepository;
        public UpdateContactHandler(IMongoRepository<Contact> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<UpdateContactResponse> Handle(UpdateContactRequest request, CancellationToken cancellationToken)
        {
            var query = await _mongoRepository.FindAsync(x => x.Id == request.Id);

            var contact = query.FirstOrDefault();

            if (contact == null)
            {
                throw new ValidationException("contact not found");
            }

            contact.Name = request.Name;
            contact.Phone = request.Phone;

            await _mongoRepository.ReplaceOneAsync(x => x.Id == request.Id, contact);

            return new UpdateContactResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Phone = contact.Phone,
            };
        }
    }
}
