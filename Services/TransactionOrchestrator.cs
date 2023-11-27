namespace Services;

public class TransactionOrchestrator
{
    readonly Transactions transactions;
    readonly ITransferService transferService;

    public TransactionOrchestrator(Transactions transactions, ITransferService transferService)
    {
        this.transactions = transactions;
        this.transferService = transferService;
    }

    public void DraftTransfer(string transactionId, string creditAccountId, string debitAccountId, decimal amount,
        DateTime transactionDate, string description)
    {
        if (amount < 0)
            throw new InvalidOperationException("Transfer amount can not be negative");
        transactions.Add(Transaction.Draft(transactionId, transactionDate, description, creditAccountId, debitAccountId,
            amount));
    }

    public void CommitTransfer(
        string transactionId)
    {
        var draft = transactions.FindById(transactionId);

        if (draft is null) throw new InvalidOperationException($"No transaction drafts with the id: {transactionId}");

        draft.Commit(transferService);

        transactions.Update(draft);
    }
}