public class TransactionIdFormatException : DomainException
{
    public TransactionIdFormatException() : base("Transaction id cannot be null or empty.") { }
}