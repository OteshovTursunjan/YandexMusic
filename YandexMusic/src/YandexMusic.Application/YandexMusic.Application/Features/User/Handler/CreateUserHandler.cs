using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.User.Commands;
using YandexMusic.DataAccess.Authentication;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.DataAccess.Repository;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.User.Handler;

public  class CreateUserHandler : IRequestHandler<CreateUserCommand, YandexMusics.Core.Entities.Music.User>
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher _passwordHasher;
    public CreateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        this.userRepository = userRepository;
    }
    private static readonly Random _random = new Random();
    public async Task<YandexMusics.Core.Entities.Music.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

        string randomSalt = Guid.NewGuid().ToString();
        Array rolesArray = Enum.GetValues(typeof(Roles));
        var randomRole = (Roles)rolesArray.GetValue(_random.Next(rolesArray.Length));
        YandexMusics.Core.Entities.Music.User user = new YandexMusics.Core.Entities.Music.User
        {
            Name = request.userForCreationDTO.Name,
            Email = request.userForCreationDTO.Email,
            Address = request.userForCreationDTO.Address,
            PassportId = request.userForCreationDTO.PassportId,

            Salt = randomSalt,
            Password = _passwordHasher.Encrypt(
                password: request.userForCreationDTO.Password,
                salt: randomSalt),

            Role = randomRole
        };
        await userRepository.AddAsync(user);
        return user;
    }
}
