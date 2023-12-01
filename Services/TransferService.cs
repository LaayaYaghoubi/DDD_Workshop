using Services.Domain.Transaction;
using Services.Exceptions;

public class TransferService : ITransferService
{
    Accounts accounts;

    public TransferService(Accounts accounts)
    {
        this.accounts = accounts;
    }

    public void Transfer(TransferRequest transferRequest)
    {
        var creditAccount = accounts.FindById(transferRequest.CreditAccountId.Id);
        var debitAccount = accounts.FindById(transferRequest.DebitAccountId.Id);

        if (debitAccount is null)
        {
            debitAccount = new Account(transferRequest.DebitAccountId, 0);
            accounts.Add(debitAccount);
        }

        if(creditAccount is null) throw new CreditAccountNotFoundException();
        // if(debitAccount is null) throw new InvalidOperationException($"Debit account with the id '{debitAccountId}' not found.");

        creditAccount.Credit(transferRequest.Amount);
        debitAccount.Debit(transferRequest.Amount);

        accounts.Update(creditAccount);
        accounts.Update(debitAccount);


    }
}