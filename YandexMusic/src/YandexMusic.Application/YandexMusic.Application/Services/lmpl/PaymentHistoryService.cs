using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Musics;

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
            if (payment_History == null)
                throw new ArgumentNullException(nameof(payment_History));
            var res = new Payment_History()
            {
                Tarrif_TypeId = payment_History.Tarrif_TypeId,
                AccountId = payment_History.AccountId,
                CardTypeId = payment_History.CardTypeId,
                IsPaid = true
            };
            _Repository.AddAsync(res);
            return Task.FromResult(res);
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
           if(payment_History == null)
                throw new ArgumentException(nameof(payment_History));
           var res = await _Repository.GetFirstAsync(u => u.Id ==id);

            if (res == null)
               throw new ArgumentException(nameof(res));
            res.AccountId = payment_History.AccountId;
            res.Tarrif_TypeId = payment_History.Tarrif_TypeId;
            res.CardTypeId = payment_History.CardTypeId;

            return res;
        }
    }
}
