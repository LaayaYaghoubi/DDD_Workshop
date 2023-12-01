using Services.Domain.SharedValueObject;

namespace DomainTests.Doubles;

public class ATransactionId
{
    private string _id;

    public ATransactionId WithId(string id)
    {
        _id = id;
        return this;
    }

    public TransactionId Please() => new(_id);
}