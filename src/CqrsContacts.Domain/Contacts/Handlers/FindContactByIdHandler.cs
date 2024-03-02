namespace CqrsContacts.Domain.Contacts.Handlers
{
    using CqrsContacts.Domain.Common.Interfaces;
    using CqrsContacts.Domain.Contacts.Models;
    using CqrsContacts.Domain.Contacts.Queries.Requests;
    using CqrsContacts.Domain.Contacts.Queries.Responses;
    using FluentValidation;
    using MediatR;
    using MongoDB.Driver;
    using System.Threading;
    using System.Threading.Tasks;

    public class FindContactByIdHandler : IRequestHandler<FindContactByIdRequest, FindContactByIdResponse>
    {
        private readonly IMongoRepository<Contact> _mongoRepository;

        public FindContactByIdHandler(IMongoRepository<Contact> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<FindContactByIdResponse> Handle(FindContactByIdRequest request, CancellationToken cancellationToken)
        {
            var query = await _mongoRepository.FindAsync(x => x.Id == request.Id);

            var contact = query.FirstOrDefault();

            if (contact == null)
            {
                throw new ValidationException("contact not found");
            }

            return new FindContactByIdResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Phone = contact.Phone,
            };
        }
    }
}
