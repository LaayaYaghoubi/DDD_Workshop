using Domain;
using Domain.Transaction;
using Services.Exceptions;

namespace Services;

public class TransactionOrchestrator
{
    readonly Transactions transactions;
    readonly ITransferService transferService;
    readonly DateTimeService dateTimeService;

    public TransactionOrchestrator(Transactions transactions, ITransferService transferService,
        DateTimeService dateTimeService)
    {
        this.transactions = transactions;
        this.transferService = transferService;
        this.dateTimeService = dateTimeService;
    }

    public void DraftTransfer(DraftTransferCommand command)
    {
        var transaction = transactions.FindById(command.TransactionId);

        if (transaction is not null) throw new DuplicateTransactionIdException();

        var parties = new TransactionParties(command.CreditAccountId, command.DebitAccountId);
        var request = new TransferRequest(parties, command.Amount);
        var draft = Transaction.Draft(command.TransactionId, request);
        transactions.Add(draft);
    }

    public void CommitTransfer(CommitTransferCommand command)
    {
        var draft = transactions.FindById(command.TransactionId);

        if (draft is null) throw new DraftTransactionNotFoundException();

        if (draft.Status == TransferStatus.Commit) throw new AlreadyCommittedException();

        draft.Commit(dateTimeService.Now, transferService);

        transactions.Update(draft);
    }
}