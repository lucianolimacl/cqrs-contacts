using CqrsContacts.Domain.Contacts.Commands.Requests;

namespace CqrsContacts.Api.ViewModels;

public class UpdateContactViewModel
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public UpdateContactRequest ToRequest(string id)
    {
        return new UpdateContactRequest
        {
            Id = id,
            Name = Name,
            Phone = Phone
        };
    }
}