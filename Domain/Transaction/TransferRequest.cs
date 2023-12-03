using Domain.SharedValueObject;

namespace Domain.Transaction;

public class TransferRequest
{
    public TransactionParties Parties { get; }
    public Money Amount { get; }
    public TransferRequest(TransactionParties parties, Money amount)
    {
        Parties = parties;
        Amount = amount;
    }
}