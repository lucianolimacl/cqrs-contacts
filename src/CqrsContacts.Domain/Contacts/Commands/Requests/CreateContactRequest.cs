namespace CqrsContacts.Domain.Contacts.Commands.Requests
{
    using CqrsContacts.Domain.Contacts.Commands.Responses;
    using MediatR;

    public class CreateContactRequest : IRequest<CreateContactResponse>
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
