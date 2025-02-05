using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.Application.Features.Account.Commands;

public record DeletAccountCommand(Guid id) : IRequest<bool>;
