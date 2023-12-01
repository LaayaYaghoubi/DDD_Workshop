using Services.Domain.SharedValueObject;
using Services.Exceptions;

namespace Services.Domain.Transaction;

public class TransferRequest
{
    public TransferRequest(AccountId creditAccountId, AccountId debitAccountId, decimal amount)
    {
        if (amount <= 0)
            throw new TransferAmountCanNotBeNegativeOrZeroException();

        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
        Amount = amount;
    }

    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }
    public Money Amount { get; }
    
}