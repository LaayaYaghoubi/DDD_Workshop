using Services.Domain.SharedValueObject;
using Services.Domain.Transaction;

public class Transaction
{
    public TransactionId Id { get; private set; }
    public DateTime Date { get; private set; }
    public TransferRequest TransferRequest { get; private set; }
    public string? Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    private Transaction(TransactionId id,
        DateTime date,
        TransferRequest details)
    {
        Id = id;
        Date = date;
        TransferRequest = details;
    }

    public static Transaction Draft(
        TransactionId id,
        DateTime date,
        TransferRequest details)
        => new(
            id,
            date,
            details
        );


    public void Describe(string description) => Description = description;

    public void Commit(ITransferService transferService)
    {
        transferService.Transfer(TransferRequest);
        Status = TransferStatus.Commit;
    }
}

