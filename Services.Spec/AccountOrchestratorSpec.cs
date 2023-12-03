using AutoFixture.Xunit2;
using Domain.Account;
using Domain.SharedValueObject.Exceptions;
using FluentAssertions;
using Services.Exceptions;

namespace Services.Spec;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(string accountId, decimal balance,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryAccounts __,
        AccountQueries queries,
        AccountOrchestrator accountOrchestrator
    )
    {
        accountOrchestrator.OpenAccount(accountId, Math.Abs(balance));
        queries.GetBalanceForAccount(accountId).Should().BeEquivalentTo(new { Balance = Math.Abs(balance) });
    }


    [Theory, AutoMoqData]
    public void Opens_a_new_account_with_negative_initial_balance_fails(
        string accountId,
        decimal balance,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryAccounts __,
        AccountOrchestrator accountOrchestrator
    )
    {
        var openAccountAction = () => accountOrchestrator.OpenAccount(accountId, -Math.Abs(balance));

        openAccountAction.Should()
            .ThrowExactly<MoneyCanNotBeNegativeException>();
    }


    [Theory, AutoMoqData]
    public void Opens_a_new_account_with_same_account_id_fails(
        string accountId,
        decimal balance,
        [Frozen(Matching.ImplementedInterfaces)] InMemoryAccounts __,
        AccountOrchestrator accountOrchestrator
    )
    {
        var account = Build.AnAccount.WithId(accountId).WithBalance(Math.Abs(balance)).Please();
        accountOrchestrator.OpenAccount(account.Id.Id, account.Balance.Value);

        var openAccountAction = () => accountOrchestrator.OpenAccount(accountId, Math.Abs(balance));

        openAccountAction.Should()
            .ThrowExactly<DuplicateAccountIdException>();
    }
}