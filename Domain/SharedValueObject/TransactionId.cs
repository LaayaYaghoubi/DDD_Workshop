using Domain.SharedValueObject.Exceptions;

namespace Domain.SharedValueObject;

public class TransactionId
{
    public string Id { get; }

    public TransactionId(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new IdCanNotBeNullOrEmptyException();
        Id = id;
    }

    public static implicit operator TransactionId(string id) => new(id);
}