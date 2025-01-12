using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        public readonly IPayment_HistoryRepository _Repository;
        public PaymentHistoryService(IPayment_HistoryRepository repository)
        {
            _Repository = repository;
        }

        public Task<Payment_History> AddPaymentAsync(PaymentHistoryDTO payment_History)
        {
            throw new NotImplementedException();

        }

        public Task<bool> DeletePaymentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PaymentHistoryDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentHistoryDTO> GetByIdAsync(Guid id)
        {
            if(id == null)
                throw new ArgumentNullException(nameof(id));
            var res = await _Repository.GetFirstAsync(u => u.Id == id);
            return new PaymentHistoryDTO
            {
                AccountId = res.AccountId,
                CardTypeId = res.CardTypeId,
                Tarrif_TypeId = res.Tarrif_TypeId,
            };
        }

        public async Task<Payment_History> UpdatePaymentAsync(Guid id, PaymentHistoryDTO payment_History)
        {
            throw new NotImplementedException();

        }
    }
}
