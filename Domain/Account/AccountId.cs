
using Domain.SharedValueObject;
using Domain.SharedValueObject.Exceptions;

namespace Domain.Account;

public class AccountId : ValueObject
{
    public string Id { get; }
    public AccountId(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new IdCanNotBeNullOrEmptyException();
        Id = id;
    }
    public static implicit operator AccountId(string id)
        => new AccountId(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}