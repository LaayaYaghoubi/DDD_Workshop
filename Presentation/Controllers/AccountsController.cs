using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    readonly AccountOrchestrator accountOrchestrator;
    public AccountsController(AccountOrchestrator accountOrchestrator)
    {
        this.accountOrchestrator = accountOrchestrator;
    }
    [HttpPost]
    public void OpenAccount(OpenAccountCommand command)
        => accountOrchestrator.OpenAccount(command);
}