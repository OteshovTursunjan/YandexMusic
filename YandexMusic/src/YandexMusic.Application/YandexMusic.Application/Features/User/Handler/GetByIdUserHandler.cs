using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.User.Queries;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.User.Handler
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQueries, YandexMusics.Core.Entities.Music.User>
    {
        private readonly IUserRepository userRepository;
        public GetByIdUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<YandexMusics.Core.Entities.Music.User> Handle(GetByIdUserQueries request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetFirstAsync(u => u.Id == request.id);
            if (user == null) return null;
            return user;
        }
    }
}
