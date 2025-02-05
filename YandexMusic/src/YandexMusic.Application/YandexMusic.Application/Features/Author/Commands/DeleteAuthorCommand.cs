using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace YandexMusic.Application.Features.Author.Commands;

public record DeleteAuthorCommand(Guid id) : IRequest<bool>;

