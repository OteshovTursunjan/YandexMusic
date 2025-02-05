using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.PaymentHistory.Queries;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.PaymentHistory.Handler;

public  class GetPaymentHistoryHandler : IRequestHandler<GetPaymentHistoryByIdQueries, Payment_History>
{
    private readonly IPayment_HistoryRepository payment_HistoryRepository;
    public GetPaymentHistoryHandler(IPayment_HistoryRepository payment_HistoryRepository)
    {
        this.payment_HistoryRepository = payment_HistoryRepository;
    }

    public async Task<Payment_History> Handle(GetPaymentHistoryByIdQueries request, CancellationToken cancellationToken)
    {
        var payment = await payment_HistoryRepository.GetFirstAsync(u => u.Id == request.id);
        if (payment == null) return null;
        return payment;
    }
}
