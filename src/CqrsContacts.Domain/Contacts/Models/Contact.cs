namespace CqrsContacts.Domain.Contacts.Models
{
    using CqrsContacts.Domain.Common.Models;
    public class Contact : BaseDocumentModel
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
