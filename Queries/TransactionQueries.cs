public record TransferDraftViewModel(
    string creditAccountId,
    string debitAccountId,
    decimal amount,
    DateTime date);


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
            t.CreditAccountId,
            t.DebitAccountId,
            t.Amount,
            t.Date
        ));

}