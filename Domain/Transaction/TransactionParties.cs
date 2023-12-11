using Domain.Account;

namespace Domain.Transaction;

public class TransactionParties
{
    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }

    public TransactionParties(AccountId creditAccountId, AccountId debitAccountId)
    {
        if (creditAccountId == debitAccountId) throw new SameTransactionPartiesException();

        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
    }


}