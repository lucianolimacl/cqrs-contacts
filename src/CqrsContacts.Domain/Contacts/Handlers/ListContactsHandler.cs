namespace CqrsContacts.Domain.Contacts.Handlers;

using CqrsContacts.Domain.Common.Interfaces;
using CqrsContacts.Domain.Contacts.Models;
using CqrsContacts.Domain.Contacts.Queries.Requests;
using CqrsContacts.Domain.Contacts.Queries.Responses;
using CqrsContacts.Domain.Contacts.Utils;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
public class ListContactsHandler : IRequestHandler<ListContactsRequest, ListContactsResponse>
{
    private readonly IMongoRepository<Contact> _mongoRepository;

    public ListContactsHandler(IMongoRepository<Contact> mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }

    public async Task<ListContactsResponse> Handle(ListContactsRequest request, CancellationToken cancellationToken)
    {
        var query = await _mongoRepository.FindAsync();

        var contacts = query.Select(x => new BaseContactResponse
        {
            Id = x.Id,
            Name = x.Name,
            Phone = x.Phone
        });

        return new ListContactsResponse { Contacts = contacts.ToList() };
    }
}
