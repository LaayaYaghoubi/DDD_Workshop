

using Domain.Transaction;

public class InMemoryTransactions : Transactions
{
    public List<Transaction> records { get; set; } = new();
    public void Add(Transaction transaction)
    => records.Add(transaction);

    public Transaction? FindById(TransactionId id)
    => All().FirstOrDefault(tx => tx.Id.Id == id.Id);

    public IEnumerable<Transaction> All()
    => records;

    public void Update(Transaction draft)
    {

    }
}