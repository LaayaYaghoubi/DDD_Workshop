namespace Domain.Transaction;

public class CanNotTransferZeroAmountException : DomainException
{
    public CanNotTransferZeroAmountException() : base("Can not transfer zero amount")
    {
    }
}