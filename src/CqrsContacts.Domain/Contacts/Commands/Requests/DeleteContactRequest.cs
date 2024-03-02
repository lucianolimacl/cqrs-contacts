namespace CqrsContacts.Domain.Contacts.Commands.Requests
{
    using CqrsContacts.Domain.Contacts.Commands.Responses;
    using MediatR;

    public class DeleteContactRequest : IRequest<DeleteContactResponse>
    {
        public string Id { get; set; } = null!;
    }
}
