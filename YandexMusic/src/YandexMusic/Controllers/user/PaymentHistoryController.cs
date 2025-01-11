using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;

namespace YandexMusic.Controllers.user
{
    public class PaymentHistoryController : Controller
    {
        public readonly IPaymentHistoryService _paymentHistoryService;
        public PaymentHistoryController(IPaymentHistoryService paymentHistoryService)
        {
            _paymentHistoryService = paymentHistoryService;
        } 
 
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetPayment{id}")]

        public async Task<IActionResult> GetPayment(Guid id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var payment = await _paymentHistoryService.GetByIdAsync(id);
            return payment == null ? NotFound() : Ok(payment);
        }
        [HttpPost("AddPayment")]
        public async Task<IActionResult> AddPaymnet(PaymentHistoryDTO paymentHistoryDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var payment  = await _paymentHistoryService.AddPaymentAsync(paymentHistoryDTO);
            return Ok(payment);
        }
       
    }
}
