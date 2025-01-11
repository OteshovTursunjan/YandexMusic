using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Services.lmpl
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        public readonly IPayment_HistoryRepository _Repository;
        public readonly IAccountRepository _accountRepository;
        public readonly  ITarrift_TypeRepository _tarrift_TypeRepository;
        public PaymentHistoryService(IPayment_HistoryRepository repository, IAccountRepository accountRepository,
            ITarrift_TypeRepository tarrift_TypeRepository)
        {
            _Repository = repository;
            _accountRepository = accountRepository;
            _tarrift_TypeRepository = tarrift_TypeRepository;
        }

        public async Task<bool> AddPaymentAsync(PaymentHistoryDTO payment_History)
        {
            if (payment_History == null)
                throw new ArgumentNullException(nameof(payment_History));
            var accountBallance = await _accountRepository.GetFirstAsync(u => u.Id == payment_History.AccountId);
            var tarrif = await _tarrift_TypeRepository.GetFirstAsync(u => u.Id == payment_History.Tarrif_TypeId);
            if (accountBallance.Balance >= tarrif.Amount)
            {
                accountBallance.Balance -= tarrif.Amount;
                var res = new Payment_History()
                {
                    Tarrif_TypeId = payment_History.Tarrif_TypeId,
                    AccountId = payment_History.AccountId,
                    CardTypeId = payment_History.CardTypeId,
                    IsPaid = true
                };
                await _Repository.AddAsync(res);
                return true;
            }
            else
            {
                return false;
            }
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
