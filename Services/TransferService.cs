public class TransferService : ITransferService
{
    Accounts accounts;

    public TransferService(Accounts accounts)
    {
        this.accounts = accounts;
    }

    public void Transfer(string creditAccountId, string debitAccountId, Money amount)
    {
        var creditAccount = accounts.FindById(creditAccountId);
        var debitAccount = accounts.FindById(debitAccountId);

        if (debitAccount is null)
        {
            debitAccount = new Account(debitAccountId, 0);
            accounts.Add(debitAccount);
        }

        if(creditAccount is null) throw new InvalidOperationException($"Credit account with the id '{creditAccountId}' not found.");
        // if(debitAccount is null) throw new InvalidOperationException($"Debit account with the id '{debitAccountId}' not found.");

        creditAccount.Credit(amount);
        debitAccount.Debit(amount);

        accounts.Update(creditAccount);
        accounts.Update(debitAccount);


    }
}