using Domain.Transaction;

public record TransferDraftViewModel(
    string creditAccountId,
    string debitAccountId,
    decimal balance);


public class TransactionQueries
{
    readonly Transactions transactions;
    public TransactionQueries(Transactions transactions)
    {
        this.transactions = transactions;
    }
    public IEnumerable<TransferDraftViewModel> AllDrafts()
    => transactions.All()
        .Where(t => t.Status == TransferStatus.Draft)
        .Select(t => new TransferDraftViewModel(
            t.TransferRequest.Parties.CreditAccountId.Id,
            t.TransferRequest.Parties.DebitAccountId.Id,
            t.TransferRequest.Amount.Value
        ));

}