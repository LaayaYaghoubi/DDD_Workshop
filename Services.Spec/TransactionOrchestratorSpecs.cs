using AutoFixture.Xunit2;
using FluentAssertions;

namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        string debitAccountId,
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator orchestrator,
        AccountOrchestrator accountOrchestrator,
        TransactionQueries queries
    )
    {
        var creditAccount = Build.AnAccount.WithBalance(20000).Please();

        accountOrchestrator.OpenAccount(creditAccount.Id, creditAccount.Balance);

        orchestrator.Transfer(creditAccount.Id, debitAccountId, 10000);

        queries.GetBalanceForAccount(debitAccountId).Should().BeEquivalentTo(new { Balance = 10000 });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator orchestrator,
        AccountOrchestrator accountOrchestrator,
        TransactionQueries queries)
    {
        var creditAccount = Build.AnAccount.WithBalance(25000).Please();

        accountOrchestrator.OpenAccount(creditAccount.Id, creditAccount.Balance);

        orchestrator.Transfer(creditAccount.Id, "dummy", 10000);

        queries.GetBalanceForAccount(creditAccount.Id).Should().BeEquivalentTo(new { Balance = 15000 });
    }

    [Theory, AutoMoqData]
    public void Transfer_negative_amount_fails(
        TransactionOrchestrator orchestrator,
        AccountOrchestrator accountOrchestrator)
    {
        var creditAccount = Build.AnAccount.Please();

        accountOrchestrator.OpenAccount(creditAccount.Id, creditAccount.Balance);

        var transferAction = () => orchestrator.Transfer(creditAccount.Id, "debitAccountId", -10000);

        transferAction.Should().Throw<InvalidOperationException>("Transfer amount can not be negative");
    }

    [Theory, AutoMoqData]
    public void Transfer_amount_greater_than_credit_balance_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator orchestrator,
        AccountOrchestrator accountOrchestrator)
    {
        var creditAccount = Build.AnAccount.WithBalance(5000).Please();

        accountOrchestrator.OpenAccount(creditAccount.Id, creditAccount.Balance);

        var transferAction = () => orchestrator.Transfer(creditAccount.Id, "debitAccountId", 10000);

        transferAction.Should().Throw<InvalidOperationException>("No enough charge");
    }

    [Theory, AutoMoqData]
    public void Transfer_from_nonexistent_credit_account_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator orchestrator,
        string dummyCreditAccountId)
    {
        var transferAction = () => orchestrator.Transfer(dummyCreditAccountId, "debitAccountId", 10000);

        transferAction.Should()
            .Throw<InvalidOperationException>($"Credit account with the id '{dummyCreditAccountId}' not found.");
    }

  
}