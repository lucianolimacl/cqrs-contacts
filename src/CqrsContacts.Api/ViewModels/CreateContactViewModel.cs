using CqrsContacts.Domain.Contacts.Commands.Requests;

namespace CqrsContacts.Api.ViewModels;

public class CreateContactViewModel
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public CreateContactRequest ToRequest()
    {
        return new CreateContactRequest
        {
            Name = Name,
            Phone = Phone
        };
    }
}

