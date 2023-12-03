using System.ComponentModel.DataAnnotations;
using AutoFixture.Xunit2;
using Domain.SharedValueObject.Exceptions;
using DomainTests.Doubles;
using FluentAssertions;

namespace DomainTests;

public class TransferRequestSpecs
{
    [Theory, AutoData]
    public void transfer_amount_can_be_positive(
        string creditAccountId,
        string debitAccountId,
        decimal amount)
    {
        var transferRequest = Build.ATransferRequest
            .WithParties(creditAccountId, debitAccountId)
            .WithAmount(amount)
            .Please();

        transferRequest.Amount.Value.Should().Be(amount);
        transferRequest.Parties.CreditAccountId.Id.Should().Be(creditAccountId);
        transferRequest.Parties.DebitAccountId.Id.Should().Be(debitAccountId);
    }

    [Theory, AutoData]
    public void transfer_amount_can_not_be_zero(
        string creditAccountId,
        string debitAccountId,
        [Range(0.0, 0.0)] decimal amount)
        => new Action(() =>
                
                Build.ATransferRequest
                    .WithParties(creditAccountId, debitAccountId)
                    .WithAmount(amount)
                    .Please())
            
            .Should().Throw<MoneyCanNotBeNegativeException>();
    
    [Theory, AutoData]
    public void transfer_amount_can_not_be_negative(
        string creditAccountId,
        string debitAccountId, 
        decimal amount)
        => new Action(() =>
                
                Build.ATransferRequest
                    .WithParties(creditAccountId, debitAccountId)
                    .WithAmount(amount.ConvertToNegative())
                    .Please())
            
            .Should().Throw<MoneyCanNotBeNegativeException>();
}