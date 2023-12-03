using AutoFixture.Xunit2;
using Domain.Account;
using Domain.Transaction;
using FluentAssertions;
using Services.Exceptions;

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
        decimal amount
    )
    {
        amount = Math.Abs(amount);

        accountOrchestrator.OpenAccount(creditAccountId, amount + 20000);

        sut.DraftTransfer(
            transactionId,
            creditAccountId,
            debitAccountId,
            amount);

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
        string debitAccountId
    )
    {
        amount = Math.Abs(amount);
        var creditAccount = Build.AnAccount.WithBalance(amount + 25000).Please();

        accountService.OpenAccount(creditAccount.Id.Id, creditAccount.Balance.Value);

        sut.DraftTransfer(
            transactionId,
            creditAccount.Id.Id,
            debitAccountId,
            amount);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(creditAccount.Id.Id).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction(
        [Frozen] Transactions _,
        TransactionOrchestrator sut,
        TransactionQueries queries,
        string creditAccountId,
        string debitAccountId,
        decimal amount,
        string transactionId)
    {
        amount = Math.Abs(amount);

        sut.DraftTransfer(
            transactionId,
            creditAccountId,
            debitAccountId,
            amount);

        queries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            creditAccountId,
            debitAccountId,
            amount
        ));
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction_with_duplicate_transaction_identity_fails(
        string debitAccountId,
        string creditAccountId,
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountOrchestrator,
        string transactionId,
        decimal amount
    )
    {
        amount = Math.Abs(amount);

        accountOrchestrator.OpenAccount(creditAccountId, amount + 20000);

        sut.DraftTransfer(
            transactionId,
            creditAccountId,
            debitAccountId,
            amount);

        var draftAction = () =>  sut.DraftTransfer(
            transactionId,
            creditAccountId,
            debitAccountId,
            amount);

        draftAction.Should().ThrowExactly<DuplicateTransactionIdException>();
    }

    [Theory, AutoMoqData]
    public void Transfer_negative_amount_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        decimal amount,
        string transactionId,
        string debitAccountId)
    {
        var negativeAmount = -Math.Abs(amount);
        var creditAccount = Build.AnAccount.Please();

        accountService.OpenAccount(creditAccount.Id.Id, creditAccount.Balance.Value);

        var transferAction = () =>
            sut.DraftTransfer(
                transactionId, 
                creditAccount.Id.Id, 
                debitAccountId, 
                
                negativeAmount);

        transferAction.Should().ThrowExactly<TransferAmountCanNotBeNegativeOrZeroException>();
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
        string debitAccountId,
        decimal five)
    {
        amount = Math.Abs(amount);
        five = Math.Abs(five);
        var creditAccount = Build.AnAccount.WithBalance(amount).Please();

        accountService.OpenAccount(creditAccount.Id.Id, creditAccount.Balance.Value);

        sut.DraftTransfer(
            transactionId,
        creditAccount.Id.Id, 
            debitAccountId,
            (amount + five));

        var transferAction = () => sut.CommitTransfer(transactionId);

        transferAction.Should().ThrowExactly<NoEnoughChargeException>();
    }

    [Theory, AutoMoqData]
    public void Transfer_from_nonexistent_credit_account_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        string transactionId,
        decimal amount,
        string debitAccountId,
        string dummyCreditAccountId)
    {
        sut.DraftTransfer(
            transactionId,
            dummyCreditAccountId, 
            debitAccountId,
            
            (amount + 1));

        var transferAction = () => sut.CommitTransfer(transactionId);

        transferAction.Should()
            .ThrowExactly<CreditAccountNotFoundException>();
    }

    [Theory, AutoMoqData]
    public void Transfer_from_nonexistent_transaction_fails(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        string transactionId,
        decimal amount)
    {
        amount = Math.Abs(amount);
        var creditAccount = Build.AnAccount.WithBalance(amount + 25000).Please();

        accountService.OpenAccount(creditAccount.Id.Id, creditAccount.Balance.Value);

        var transferAction = () => sut.CommitTransfer(transactionId);

        transferAction.Should()
            .ThrowExactly<DraftTransactionNotFoundException>();
    }

    [Theory, AutoMoqData]
    public void Committed_transaction_can_not_commit_again(
        string debitAccountId,
        string creditAccountId,
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)]
        TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountOrchestrator,
        string transactionId,
        decimal amount
    )
    {
        amount = Math.Abs(amount);

        accountOrchestrator.OpenAccount(creditAccountId, amount + 20000);

        sut.DraftTransfer(
            transactionId,
            creditAccountId, 
            debitAccountId,
            amount);

        sut.CommitTransfer(transactionId);

        var commitAction = () => sut.CommitTransfer(transactionId);

        commitAction.Should().ThrowExactly<AlreadyCommittedException>();
    }
}