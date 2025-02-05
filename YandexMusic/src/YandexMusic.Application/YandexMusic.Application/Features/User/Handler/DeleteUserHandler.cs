using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.User.Commands;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.User.Handler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository userRepository;
        public DeleteUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
           var user = await userRepository.GetFirstAsync(u => u.Id == request.id);
            if (user == null) return false;
            await userRepository.DeleteAsync(user); 
            return true;
        }
    }
}
