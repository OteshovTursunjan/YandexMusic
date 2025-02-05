using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Application.Features.Cards.Commands;
public record CreateCardsCommand(CardDTO CardDTO) : IRequest<bool>;

