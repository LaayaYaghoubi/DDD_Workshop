using Services.Exceptions;

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
            throw new NoEnoughChargeException();

        Balance -= amount;
    }

    public void Debit(Money amount)
    {
        Balance += amount;
    }
}