using Services.Domain.Transaction;

public interface ITransferService
{
    void Transfer(TransferRequest transferRequest);
}