using Domain;
using Domain.Transaction;
using Services.Exceptions;

namespace Services;

public class TransactionOrchestrator
{
    readonly Transactions transactions;
    readonly ITransferService transferService;
    private readonly DateTimeService dateTimeService;

    public TransactionOrchestrator(Transactions transactions, ITransferService transferService,
        DateTimeService dateTimeService)
    {
        this.transactions = transactions;
        this.transferService = transferService;
        this.dateTimeService = dateTimeService;
    }

    public void DraftTransfer(string transactionId,
        string creditAccountId,
        string debitAccountId,
        decimal amount)
    {
        var transaction = transactions.FindById(transactionId);

        if (transaction is not null) throw new DuplicateTransactionIdException();

        var parties = new TransactionParties(creditAccountId, debitAccountId);
        var request = new TransferRequest(parties, amount);
        var draft = Transaction.Draft(transactionId, request);
        transactions.Add(draft);
    }

    public void CommitTransfer(
        string transactionId)
    {
        var transaction = transactions.FindById(transactionId);

        if (transaction is null) throw new DraftTransactionNotFoundException();

        if (transaction.Status == TransferStatus.Commit) throw new AlreadyCommittedException();

        transaction.Commit(dateTimeService.Now, transferService);

        transactions.Update(transaction);
    }
}