public class SameTransactionPartiesException : DomainException
{
    public SameTransactionPartiesException() : base("Credit account and debit account cannot be the same")
    {
    }
}