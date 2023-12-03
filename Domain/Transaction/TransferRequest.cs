using Domain.SharedValueObject;
using Domain.SharedValueObject.Exceptions;

namespace Domain.Transaction;

public class TransferRequest
{
    public TransactionParties Parties { get; }
    public Money Amount { get; }

    public TransferRequest(TransactionParties parties, Money amount)
    {
        if (amount <= 0) throw new MoneyCanNotBeNegativeException();
        Parties = parties;
        Amount = amount;
    }
}