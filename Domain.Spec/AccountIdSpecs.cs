using DomainTests.Doubles;
using FluentAssertions;

namespace Domain.Spec;

public class AccountIdSpecs
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void AccountId_can_not_be_null_or_empty(string id)
    {
        Action action = () => Build.AnAccountId.WithId(id).Please();
    
        action.Should().ThrowExactly<AccountIdFormatException>();
    }
}
