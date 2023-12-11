
using DomainTests.Doubles;
using FluentAssertions;

namespace DomainTests;

public class TransactionIdSpecs
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void TransactionId_can_not_be_null_or_empty(string id)
    {
        Action action = () => Build.ATransactionId.WithId(id).Please();
    
        action.Should().ThrowExactly<TransactionIdFormatException>();
    }
}