using Domain.Account;
using Services.Exceptions;

public class AccountOrchestrator
{
    private Accounts accounts;

    public AccountOrchestrator(Accounts accounts)
    {
        this.accounts = accounts;
    }

    public void OpenAccount(string accountId, decimal initialBalance)
    {
        if (accounts.IsExist(accountId))
        {
            throw new DuplicateAccountIdException();
        }

        accounts.Add(new Account(accountId, initialBalance));
    }
}