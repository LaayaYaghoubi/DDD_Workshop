public class NegativeMoneyException : DomainException
{
    public NegativeMoneyException() : base("Money cannot be negative.") { }
}