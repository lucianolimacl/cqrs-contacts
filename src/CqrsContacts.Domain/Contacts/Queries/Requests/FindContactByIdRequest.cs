namespace CqrsContacts.Domain.Contacts.Queries.Requests;

using CqrsContacts.Domain.Contacts.Queries.Responses;
using MediatR;

public class FindContactByIdRequest : IRequest<FindContactByIdResponse>
{
    public string Id { get; set; } = null!;
}
