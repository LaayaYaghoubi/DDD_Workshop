using Services.Domain.SharedValueObject;

public class Accounts
{
    readonly List<Account> records = new List<Account>();

    public Account? FindById(string id)
    {
        return records.FirstOrDefault(a => a.Id.Id == id);
    }

    public bool IsExist(string id)
    {
        return records.Any(a => a.Id.Id == id);
    }

    public void Update(Account account)
    {
        var record = FindById(account.Id.Id);
    }

    public void Add(Account account)
    {
        records.Add(account);
    }

    public void Clear() => records.Clear();
}