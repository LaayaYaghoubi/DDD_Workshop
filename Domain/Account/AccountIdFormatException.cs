public class AccountIdFormatException : DomainException
{
    public AccountIdFormatException() : base("Account id is malformed") { }
}