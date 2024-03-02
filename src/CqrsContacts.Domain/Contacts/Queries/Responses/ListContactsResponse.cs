namespace CqrsContacts.Domain.Contacts.Queries.Responses
{
    using CqrsContacts.Domain.Contacts.Utils;
    public class ListContactsResponse 
    {
        public List<BaseContactResponse> Contacts { get; set; } = null!;
    }
}
