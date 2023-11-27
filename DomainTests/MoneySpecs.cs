using AutoFixture.Xunit2;
using DomainTests.Doubles;
using FluentAssertions;

namespace DomainTests;

public class MoneySpecs
{
    static Money AValidMoney() => FixtureBuilder.A<Money>(with => new Money(Math.Abs(with.Value)));


    [Theory, AutoData]
    public void Money_can_be_positive(decimal amount)
    {
        Build.AMoney.WithPositiveValue(amount).Please()
            
            .Value.Should().Be(amount);
    }

    [Theory, AutoData]
    public void Money_can_be_zero(decimal amount)
    {
        Build.AMoney.WithPositiveValue(amount - amount).Please()
            
            .Value.Should().Be(amount - amount);
    }

    [Theory, AutoData]
    public void Money_cannot_be_negative(decimal amount)
        => new Action(() => 
                
                Build.AMoney.WithNegativeValue(amount).Please()) 
            
            .Should().Throw<Exception>();

    [Theory, AutoData]
    public void Supports_subtraction_when_left_is_greater(uint five)
    {
        var right = AValidMoney();
        var left = Build.AMoney.WithPositiveValue(right.Value + five).Please();
        
        (left - right)
            
            .Value.Should().Be(five);
    }

    [Fact]
    public void Supports_subtraction_when_left_is_equal_to_right()
    {
        var right = AValidMoney();
        var left = Build.AMoney.WithPositiveValue(right.Value).Please();
        
        (left - right)
            
            .Value.Should().Be(left.Value - left.Value);
    }

    [Theory, AutoData]
    public void Does_not_support_subtraction_when_left_is_less_than_right(uint five)
    {
        var left = AValidMoney();
        var right = Build.AMoney.WithPositiveValue(left.Value + five).Please();

        
        var subtractAction = () => left - right;
        
        subtractAction.Should().Throw<Exception>();
    }

    [Fact]
    public void Supports_addition()
    {
        var left = AValidMoney();
        var right = AValidMoney();

        (left + right)
            
            .Value.Should().Be(left.Value + right.Value);
    }

    [Theory, AutoData]
    public void Supports_greater_than(decimal five)
    {
        var smallerNumber = AValidMoney();
        var biggerNumber = Build.AMoney.WithPositiveValue(smallerNumber.Value + five).Please();

        (biggerNumber > smallerNumber)
            
            .Should().Be(biggerNumber.Value > smallerNumber.Value);
    }

    [Theory, AutoData]
    public void Supports_less_than(decimal five)
    {
        var smallerNumber = AValidMoney();
        var biggerNumber = Build.AMoney.WithPositiveValue(smallerNumber.Value + five).Please();

        (smallerNumber < biggerNumber)
            
            .Should().Be(smallerNumber.Value < biggerNumber.Value);
    }

    [Theory]
    [InlineAutoData(15, 10)]
    [InlineAutoData(30, 20)]
    [InlineAutoData(0, 0)]
    public void Supports_greater_than_or_equal(decimal bigger, decimal smaller)
    {
        var biggerNumber = Build.AMoney.WithPositiveValue(bigger).Please();
        var smallerNumber = Build.AMoney.WithPositiveValue(smaller).Please();
        
        (biggerNumber >= smallerNumber)
            
            .Should().Be(bigger >= smaller);
    }

    [Theory]
    [InlineAutoData(5, 10)]
    [InlineAutoData(4, 20)]
    [InlineAutoData(0, 0)]
    public void Supports_less_than_or_equal(decimal bigger, decimal smaller)
    {
        var smallerNumber = Build.AMoney.WithPositiveValue(smaller).Please();
        var biggerNumber = Build.AMoney.WithPositiveValue(bigger).Please();
        
        (smallerNumber <= biggerNumber)
            
            .Should().Be(smaller <= bigger);
    }
}