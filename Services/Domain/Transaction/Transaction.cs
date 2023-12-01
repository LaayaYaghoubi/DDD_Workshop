using Services.Domain.SharedValueObject;
using Services.Domain.Transaction;

public class Transaction
{
    public TransactionId Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    private Transaction(TransactionId id,
        DateTime date,
        string description,
        string creditAccountId,
        string debitAccountId,
        Money amount)
    {
        Id = id;
        Date = date;
        Description = description;
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
        Amount = amount;
    }

    public static Transaction Draft(
        TransactionId id,
        DateTime date,
        string description,
        string creditAccountId,
        string debitAccountId,
        Money amount)
    => new Transaction(
        id,
        date,
        description,
        creditAccountId,
        debitAccountId,
        amount
    );

    public void Commit(ITransferService transferService)
    {
        transferService.Transfer(CreditAccountId, DebitAccountId, Amount);
        Status = TransferStatus.Commit;
    }
}