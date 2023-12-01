using Services.Domain.Transaction;

namespace DomainTests.Doubles;

public class ATransferRequest
{
    private decimal _amount;
    private string _creditAccountId;
    private string _debitAccountId;

    public ATransferRequest WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public ATransferRequest WithCreditAccountId(string creditAccountId)
    {
        _creditAccountId = creditAccountId;
        return this;
    }

    public ATransferRequest WithDebitAccountId(string debitAccountId)
    {
        _debitAccountId = debitAccountId;
        return this;
    }

    public TransferRequest Please() => new(_creditAccountId, _debitAccountId, _amount);
}