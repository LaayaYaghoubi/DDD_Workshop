using Domain.Account;
using Services.Exceptions;

public class AccountOrchestrator
{
    private Accounts accounts;

    public AccountOrchestrator(Accounts accounts)
    {
        this.accounts = accounts;
    }

    public void OpenAccount(OpenAccountCommand command)
    {
        if (accounts.IsExist(command.AccountId))
        {
            throw new DuplicateAccountIdException();
        }

        accounts.Add(new Account(command.AccountId, command.InitialBalance));
    }
}