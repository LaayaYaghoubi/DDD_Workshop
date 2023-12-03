namespace Domain.Transaction;

public interface Transactions
{
    void Add(Transaction transaction);
    Transaction? FindById(TransactionId id);
    void Update(Transaction draft);
    IEnumerable<Transaction> All();

}