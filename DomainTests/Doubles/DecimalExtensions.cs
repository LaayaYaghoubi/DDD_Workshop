namespace DomainTests.Doubles
{
    public static class DecimalExtensions
    {
        public static decimal ConvertToNegative(this decimal amount) =>
            -Math.Abs(amount);
    }
}