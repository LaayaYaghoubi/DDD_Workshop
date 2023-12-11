

namespace Domain.Transaction;

public class TransferRequest
{
    public TransactionParties Parties { get; }
    public Money Amount { get; }

    public TransferRequest(TransactionParties parties, Money amount)
    {
        if (amount.Value == 0) throw new CanNotTransferZeroAmountException();
        Parties = parties;
        Amount = amount;
    }
}