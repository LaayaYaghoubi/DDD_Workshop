

public class Transactions
{
    public List<Transaction> records { get; set; } = new();
    public void Add(Transaction transaction)
    => records.Add(transaction);

    public Transaction? FindById(string id)
    => All().FirstOrDefault(tx => tx.Id == id);

    public IEnumerable<Transaction> All()
    => records;

    public void Update(Transaction draft)
    {

    }
}