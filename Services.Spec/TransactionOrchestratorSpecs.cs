using AutoFixture.Xunit2;
using FluentAssertions;

namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        string debitAccountId,
        string creditAccountId,
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountOrchestrator,
        AccountQueries queries,
        string transactionId,
        decimal amount,
        DateTime now,
        string description
    )
    {
        amount = Math.Abs(amount);

        accountOrchestrator.OpenAccount(creditAccountId, amount + 20000);

        sut.DraftTransfer(transactionId,
            creditAccountId, debitAccountId,
            amount, now, description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(debitAccountId).Should()
            .BeEquivalentTo(new { Balance = amount });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        AccountQueries queries,
        string transactionId,
        decimal amount,
        DateTime now,
        string description,
        string debitAccountId
    )
    {
        amount = Math.Abs(amount);
        var creditAccount = Build.AnAccount.WithBalance(amount + 25000).Please();

        accountService.OpenAccount(creditAccount.Id, creditAccount.Balance.Value);

        sut.DraftTransfer(transactionId,
            creditAccount.Id, debitAccountId,
            amount, now, description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(creditAccount.Id).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction(
        [Frozen] Transactions _,
        TransactionOrchestrator sut,
        TransactionQueries queries,
        DateTime now,
        string description,
        string creditAccountId,
        string debitAccountId,
        decimal amount
    )
    {
        amount = Math.Abs(amount);

        sut.DraftTransfer("transaction Id", creditAccountId, debitAccountId, amount, now, description);

        queries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            creditAccountId,
            debitAccountId,
            amount,
            now
        ));
    }

    [Theory, AutoMoqData]
    public void Transfer_negative_amount_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        decimal amount,
        DateTime now,
        string description,
        string debitAccountId)
    {
        var negativeAmount = -Math.Abs(amount);
        var creditAccount = Build.AnAccount.Please();

        accountService.OpenAccount(creditAccount.Id, creditAccount.Balance);

        var transferAction = () =>
            sut.DraftTransfer("transaction Id", creditAccount.Id, debitAccountId, negativeAmount, now, description);

        transferAction.Should().Throw<InvalidOperationException>("Transfer amount can not be negative");
    }

    [Theory, AutoMoqData]
    public void Transfer_amount_greater_than_credit_balance_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        string transactionId,
        decimal amount,
        DateTime now,
        string description,
        string debitAccountId)
    {
        amount = Math.Abs(amount);
        var creditAccount = Build.AnAccount.WithBalance(amount).Please();

        accountService.OpenAccount(creditAccount.Id, creditAccount.Balance);

        sut.DraftTransfer(transactionId,
            creditAccount.Id, debitAccountId,
            (amount + 1), now, description);

        var transferAction = () => sut.CommitTransfer(transactionId);

        transferAction.Should().Throw<InvalidOperationException>("No enough charge");
    }

    [Theory, AutoMoqData]
    public void Transfer_from_nonexistent_credit_account_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        string transactionId,
        decimal amount,
        DateTime now,
        string description,
        string debitAccountId,
        string dummyCreditAccountId)
    {
        sut.DraftTransfer(transactionId,
            dummyCreditAccountId, debitAccountId,
            (amount + 1), now, description);

        var transferAction = () => sut.CommitTransfer(transactionId);

        transferAction.Should()
            .Throw<InvalidOperationException>($"Credit account with the id '{dummyCreditAccountId}' not found.");
    }
}