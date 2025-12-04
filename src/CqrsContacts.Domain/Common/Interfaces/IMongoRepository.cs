namespace CqrsContacts.Domain.Common.Interfaces;

using MongoDB.Driver;
using System.Linq.Expressions;
using System.Threading.Tasks;
public interface IMongoRepository<T>
{
    Task InsertOneAsync(T document);
    Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T document);
    public Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null!);
    Task<List<T>> FindAsync(Expression<Func<T, bool>>? filter = null, FindOptions<T, T> options = null!);
}
