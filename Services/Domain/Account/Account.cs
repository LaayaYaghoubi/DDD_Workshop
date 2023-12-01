using Services.Domain.SharedValueObject;
using Services.Exceptions;

public class Account
{
    public Account(AccountId id, Money initialBalance)
    {
        Id = id;
        Balance = initialBalance;
    }

    public AccountId Id { get; }
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