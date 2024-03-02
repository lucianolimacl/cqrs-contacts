namespace CqrsContacts.Domain.Common.Options
{
    public class ContactDatabaseOptions
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
    }
}
