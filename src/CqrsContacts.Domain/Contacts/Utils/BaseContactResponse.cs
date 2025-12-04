namespace CqrsContacts.Domain.Contacts.Utils;

public class BaseContactResponse
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
