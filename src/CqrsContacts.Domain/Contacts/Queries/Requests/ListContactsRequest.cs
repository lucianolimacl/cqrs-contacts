namespace CqrsContacts.Domain.Contacts.Queries.Requests;

using CqrsContacts.Domain.Contacts.Queries.Responses;
using MediatR;
public class ListContactsRequest : IRequest<ListContactsResponse>
{
}
