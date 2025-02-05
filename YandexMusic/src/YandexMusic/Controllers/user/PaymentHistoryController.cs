using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using MediatR;
using YandexMusic.Application.Features.PaymentHistory.Queries;

namespace YandexMusic.Controllers.user
{
    public class PaymentHistoryController : Controller
    {
        public readonly IPaymentHistoryService _paymentHistoryService;
        public readonly IMediator mediator;
        public PaymentHistoryController(IPaymentHistoryService paymentHistoryService, IMediator mediator)
        {
            _paymentHistoryService = paymentHistoryService;
            this.mediator = mediator;
        } 
 
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetPayment{id}")]

        public async Task<IActionResult> GetPayment([FromRoute] Guid id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = await mediator.Send(new GetPaymentHistoryByIdQueries(id));
            return payment == null ? NotFound() : Ok(payment);
        }
       
        [HttpPut("UpdatePayment{id}")]
        public async Task<IActionResult> UpdatePayment([FromRoute] Guid id, [FromBody]PaymentHistoryDTO paymentHistoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = _paymentHistoryService.UpdatePaymentAsync(id,paymentHistoryDTO);
            return Ok(payment);
        }
    }
}
