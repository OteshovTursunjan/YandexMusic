using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.DTOs;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Application.Services
{
    public interface IPaymentHistoryService
    {
        Task<PaymentHistoryDTO> GetByIdAsync(Guid id);
        Task<List<PaymentHistoryDTO>> GetAllAsync();
        Task<Payment_History> AddPaymentAsync(PaymentHistoryDTO payment_History);
        Task<Payment_History> UpdatePaymentAsync(Guid id, PaymentHistoryDTO payment_History);
        Task<bool> DeletePaymentAsync(Guid id);

    }
}
