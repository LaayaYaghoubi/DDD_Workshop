using Services.Domain.Transaction;
using Services.Exceptions;

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

    public void DraftTransfer(string transactionId,
        DateTime transactionDate,
        string creditAccountId,
        string debitAccountId,
        decimal amount)
    {
        var transaction = transactions.FindById(transactionId);

        if (transaction is not null) throw new DuplicateTransactionIdException();

        var transferRequest = new TransferRequest(creditAccountId, debitAccountId, amount);


        transactions.Add(Transaction.Draft(
            transactionId,
            transactionDate,
            transferRequest));
    }

    public void CommitTransfer(
        string transactionId)
    {
        var transaction = transactions.FindById(transactionId);

        if (transaction is null) throw new DraftTransactionNotFoundException();

        if (transaction.Status == TransferStatus.Commit) throw new AlreadyCommittedException();

        transaction.Commit(transferService);

        transactions.Update(transaction);
    }
}