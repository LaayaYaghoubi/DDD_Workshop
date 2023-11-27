public interface ITransferService
{
    void Transfer(string creditAccountId, string debitAccountId, Money amount);
}