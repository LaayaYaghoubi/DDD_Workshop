using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionOrchestrator _transactionOrchestrator;
        private readonly TransactionQueries _transactionQueries;

        public TransactionController(
            TransactionOrchestrator transactionOrchestrator,
            TransactionQueries transactionQueries)
        {
            _transactionOrchestrator = transactionOrchestrator;
            _transactionQueries = transactionQueries;
        }

        [HttpPost("draft")]
        public void DraftTransfer([FromBody] AddDraftTransferDto addDraftTransferDto)
        {
            _transactionOrchestrator.DraftTransfer(
                addDraftTransferDto.TransactionId,
                addDraftTransferDto.TransactionDate,
                addDraftTransferDto.CreditAccountId,
                addDraftTransferDto.DebitAccountId,
                addDraftTransferDto.Amount);
        }

        [HttpPatch("commit/{id}")]
        public void Transfer(string id)
        {
            _transactionOrchestrator.CommitTransfer(id);
        }

        [HttpGet("drafts")]
        public IEnumerable<TransferDraftViewModel> GetAll()
        {
            return _transactionQueries.AllDrafts();
        }
    }

    public record AddDraftTransferDto(
        string TransactionId, DateTime TransactionDate, string CreditAccountId, string DebitAccountId,
        decimal Amount);
}