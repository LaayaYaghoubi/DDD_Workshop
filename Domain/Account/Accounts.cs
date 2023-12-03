namespace Domain.Account;

public interface Accounts
{
    Account? FindById(AccountId id);
    void Update(Account account);
    void Add(Account account);
    bool IsExist(string accountId);
}