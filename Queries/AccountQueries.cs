public class AccountQueries
{
    Accounts accounts;
    public AccountQueries(Accounts accounts)
    =>this.accounts = accounts;
    public BalanceViewModel? GetBalanceForAccount(string accountId)
    {
        var theAccount= accounts.FindById(accountId);
        if(theAccount is null) return null;
        return new BalanceViewModel(Id: theAccount.Id, Balance:theAccount.Balance.Value);
    }
}