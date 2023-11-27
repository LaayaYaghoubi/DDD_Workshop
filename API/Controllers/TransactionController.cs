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
                addDraftTransferDto.CreditAccountId,
                addDraftTransferDto.DebitAccountId,
                addDraftTransferDto.Amount,
                addDraftTransferDto.TransactionDate,
                addDraftTransferDto.Description);
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

    public class AddDraftTransferDto
    {
        public string TransactionId { get; set; }
        public string CreditAccountId { get; set; }
        public string DebitAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}