using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.Application.Features.Cards.Queries;

public record GetCardsByIdQueries(Guid id) : IRequest<YandexMusics.Core.Entities.Music.Cards>;
