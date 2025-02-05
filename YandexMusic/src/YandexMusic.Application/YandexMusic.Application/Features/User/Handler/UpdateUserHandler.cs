using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.User.Commands;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.User.Handler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, YandexMusics.Core.Entities.Music.User>
    {
        private readonly IUserRepository userRepository;
        public UpdateUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<YandexMusics.Core.Entities.Music.User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetFirstAsync(u => u.Id == request.id);
            if (user == null) return null;
            user.Email = request.UserDTO.Email;
            user.Name = request.UserDTO.Name;
            user.Address = request.UserDTO.Address;
            user.PassportId = request.UserDTO.PassportId;
            await userRepository.UpdateAsync(user);
            return user;
        }

     
    }
}
