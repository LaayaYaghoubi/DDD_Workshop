using Domain.Transaction;

namespace Domain;

public interface ITransferService
{
    void Transfer(TransferRequest transferRequest, DateTime dateTime);
}