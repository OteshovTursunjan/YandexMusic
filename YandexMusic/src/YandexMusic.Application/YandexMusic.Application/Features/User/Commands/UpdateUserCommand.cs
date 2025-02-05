using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.User.Commands;

public record UpdateUserCommand(Guid id, UserDTO UserDTO) : IRequest<YandexMusics.Core.Entities.Music.User>;

