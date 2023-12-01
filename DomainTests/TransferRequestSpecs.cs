using System.ComponentModel.DataAnnotations;
using AutoFixture.Xunit2;
using DomainTests.Doubles;
using FluentAssertions;
using Services.Exceptions;

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
            .WithCreditAccountId(creditAccountId)
            .WithDebitAccountId(debitAccountId)
            .WithAmount(amount)
            .Please();

        transferRequest.Amount.Value.Should().Be(amount);
        transferRequest.CreditAccountId.Id.Should().Be(creditAccountId);
        transferRequest.DebitAccountId.Id.Should().Be(debitAccountId);
    }

    [Theory, AutoData]
    public void transfer_amount_can_not_be_zero(
        string creditAccountId,
        string debitAccountId,
        [Range(0.0, 0.0)] decimal amount)
        => new Action(() =>
                
                Build.ATransferRequest
                    .WithCreditAccountId(creditAccountId)
                    .WithDebitAccountId(debitAccountId)
                    .WithAmount(amount)
                    .Please())
            
            .Should().Throw<TransferAmountCanNotBeNegativeOrZeroException>();
    
    [Theory, AutoData]
    public void transfer_amount_can_not_be_negative(
        string creditAccountId,
        string debitAccountId, 
        decimal amount)
        => new Action(() =>
                
                Build.ATransferRequest
                    .WithCreditAccountId(creditAccountId)
                    .WithDebitAccountId(debitAccountId)
                    .WithAmount(amount.ConvertToNegative())
                    .Please())
            
            .Should().Throw<TransferAmountCanNotBeNegativeOrZeroException>();
}