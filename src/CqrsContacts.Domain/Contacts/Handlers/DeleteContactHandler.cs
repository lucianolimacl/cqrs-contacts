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

    public class DeleteContactHandler : IRequestHandler<DeleteContactRequest, DeleteContactResponse>
    {
        private readonly IMongoRepository<Contact> _mongoRepository;
        private readonly IValidator<CreateContactRequest> _validator;
        public DeleteContactHandler(IMongoRepository<Contact> mongoRepository, IValidator<CreateContactRequest> validator)
        {
            _mongoRepository = mongoRepository;
            _validator = validator;
        }

        public async Task<DeleteContactResponse> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
        {
            var query = await _mongoRepository.FindAsync(x => x.Id == request.Id);

            var contact = query.FirstOrDefault();
            if (contact == null)
            {
                throw new ValidationException("contact not found");
            }

            await _mongoRepository.DeleteOneAsync(x => x.Id == request.Id);

            return new DeleteContactResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Phone = contact.Phone,
            };
        }
    }
}
