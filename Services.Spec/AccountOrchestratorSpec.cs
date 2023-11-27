using AutoFixture.Xunit2;
using FluentAssertions;

namespace Services.Spec;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(string accountId, decimal balance,
        [Frozen] Accounts _,
        AccountQueries queries,
        AccountOrchestrator accountOrchestrator
    )
    {
        accountOrchestrator.OpenAccount(accountId, Math.Abs(balance));
        queries.GetBalanceForAccount(accountId).Should().BeEquivalentTo(new { Balance = Math.Abs(balance) });
    }


    [Theory, AutoMoqData]
    public void Opens_a_new_account_with_negative_initial_balance_fails(string accountId, decimal balance,
        [Frozen] Accounts _,
        AccountOrchestrator accountOrchestrator
    )
    {
        var openAccountAction = () => accountOrchestrator.OpenAccount(accountId, -Math.Abs(balance));

        openAccountAction.Should()
            .Throw<InvalidOperationException>("Accounts cannot be initialized with negative balance.");
    }


    [Theory, AutoMoqData]
    public void Opens_a_new_account_with_same_account_id_fails(string accountId, decimal balance,
        [Frozen] Accounts _,
        AccountOrchestrator accountOrchestrator
    )
    {
        var account = Build.AnAccount.WithId(accountId).WithBalance(Math.Abs(balance)).Please();
        accountOrchestrator.OpenAccount(account.Id, account.Balance);

        var openAccountAction = () => accountOrchestrator.OpenAccount(accountId, Math.Abs(balance));

        openAccountAction.Should()
            .Throw<InvalidOperationException>($"An account with ID '{accountId}' already exists.");
    }
}