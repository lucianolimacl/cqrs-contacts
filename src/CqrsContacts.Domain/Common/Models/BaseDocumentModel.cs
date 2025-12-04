namespace CqrsContacts.Domain.Common.Models;

using MongoDB.Bson.Serialization.Attributes;
public class BaseDocumentModel
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = null!;
}
