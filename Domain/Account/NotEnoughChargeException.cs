public class NotEnoughChargeException : DomainException
{
    public NotEnoughChargeException() : base("No enough charge") { }
}