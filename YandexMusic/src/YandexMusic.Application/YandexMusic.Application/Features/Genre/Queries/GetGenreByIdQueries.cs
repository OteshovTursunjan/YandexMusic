using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.Genre.Queries;

public record GetGenreByIdQueries(Guid id): IRequest<Genres>;
