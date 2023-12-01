using Services.Exceptions;

namespace Services.Domain.SharedValueObject;

public class AccountId
{
    public string Id { get; }
    public AccountId(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new IdCanNotBeNullOrEmptyException();
        Id = id;
    }

    public static implicit operator AccountId(string id) => new(id);
}