using Domain.Transaction;

namespace Domain.Spec.Doubles;

public class ATransferRequest
{
    private decimal _amount;
    private TransactionParties _parties;

    public ATransferRequest WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public ATransferRequest WithParties(string creditAccountId, string debitAccountId)
    {
        _parties = new TransactionParties(creditAccountId, debitAccountId);
        return this;
    }

    public TransferRequest Please() => new(_parties, _amount);
}