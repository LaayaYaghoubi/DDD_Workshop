
using Domain.SharedValueObject;
using Domain.SharedValueObject.Exceptions;

namespace Domain.Transaction;

public class TransactionId : ValueObject
{
    public string Id { get; }
    public TransactionId(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new IdCanNotBeNullOrEmptyException();
        Id = id;
    }
    public static implicit operator TransactionId(string id)
        => new TransactionId(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}