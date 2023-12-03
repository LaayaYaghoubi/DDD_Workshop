using Domain.Account;

public class AnAccount
{
    string id = Guid.NewGuid().ToString();
    decimal balance = 0;
     public AnAccount WithId(string id)
     {
        this.id = id;
        return this;
     }

     public AnAccount WithBalance(decimal balance)
     {
        this.balance = balance;
        return this;
     }

     public Account Please()
     => new Account(id, balance);
     
}