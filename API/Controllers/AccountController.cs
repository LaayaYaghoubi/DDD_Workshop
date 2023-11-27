using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountOrchestrator _accountOrchestrator;
    private readonly AccountQueries _accountQueries;

    public AccountController(AccountOrchestrator accountOrchestrator,
        AccountQueries accountQueries)
    {
        _accountOrchestrator = accountOrchestrator;
        _accountQueries = accountQueries;
    }

    [HttpPost]
    public void Add(AddAccountDto dto)
    {
        _accountOrchestrator.OpenAccount(dto.Id, dto.InitialBalance);
    }

    [HttpGet("{id}")]
    public BalanceViewModel? GetBalance(string id)
    {
        return _accountQueries.GetBalanceForAccount(id);
    }

    public class AddAccountDto
    {
        public string Id { get; set; }
        public decimal InitialBalance { get; set; }
    }
}