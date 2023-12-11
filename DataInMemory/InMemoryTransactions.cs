using Domain.Transaction;
using InternalMessaging;


public class InMemoryTransactions : Transactions
{
    readonly IMessageDispatcher messageDispatcher;

    public InMemoryTransactions(IMessageDispatcher messageDispatcher)
        => this.messageDispatcher = messageDispatcher;


    public List<Transaction> records { get; set; } = new();
    public void Add(Transaction transaction)
    {
        EmitEvents(transaction);
        records.Add(transaction);
        //SaveChanges
    }

    public Transaction? FindById(TransactionId id)
        => All().FirstOrDefault(tx => tx.Id.Id == id.Id);

    public IEnumerable<Transaction> All()
        => records;

    public void Update(Transaction transaction)
    {
        EmitEvents(transaction);
    }

    public void EmitEvents(Transaction transaction)
    {
        messageDispatcher.Dispatch(transaction.NewEvents);
        transaction.ClearEvents();
    }
}