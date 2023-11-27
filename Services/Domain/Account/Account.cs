public class Account
{
    public Account(string id, Money initialBalance)
    {
        Id = id;
        Balance = initialBalance;
    }
    public string Id { get; }
    public Money Balance { get; private set; }

    public void Credit(Money amount)
    { 
        if (Balance <= amount)
            throw new InvalidOperationException("No enough charge");
        
        Balance -= amount;
    }

    public void Debit(Money amount)
    {
        Balance += amount;       
    }


}