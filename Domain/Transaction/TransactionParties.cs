using Domain.Account;

namespace Domain.Transaction;

public class TransactionParties
{
    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }

    public TransactionParties(AccountId creditAccountId, AccountId debitAccountId)
    {
        if (creditAccountId == debitAccountId) throw new InvalidOperationException("Credit account and debit account cannot be the same");
        
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
    }


}