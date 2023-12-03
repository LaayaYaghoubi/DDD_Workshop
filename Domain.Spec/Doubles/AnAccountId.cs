using Domain.Account;

namespace DomainTests.Doubles;

public class AnAccountId
{
   private string _id;

    public AnAccountId WithId(string id)
    {
        _id = id;
        return this;
    }

    public AccountId Please() => new(_id);
}