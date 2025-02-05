using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.Application.Features.User.Commands;

public record DeleteUserCommand(Guid id) : IRequest<bool>;

