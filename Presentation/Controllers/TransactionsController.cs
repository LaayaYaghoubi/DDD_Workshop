using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    readonly TransactionOrchestrator transactionOrchestrator;
    public TransactionsController(TransactionOrchestrator transactionOrchestrator)
        => this.transactionOrchestrator = transactionOrchestrator;

    [HttpPost("draft")]
    public void Draft([FromBody] DraftTransferCommand command)
        => transactionOrchestrator.DraftTransfer(command);

    [HttpPost("commit")]
    public void Commit([FromBody] CommitTransferCommand command)
        => transactionOrchestrator.CommitTransfer(command);
}