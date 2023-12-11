namespace Domain.Transaction;

public class TransferMoneyMustBeGreaterThanZeroException : DomainException
{
    public TransferMoneyMustBeGreaterThanZeroException(string? message) : base(
        "Transfer money must be greater than zero")
    {
    }
}