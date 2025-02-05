using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using YandexMusics.Core.Entities.Music;
namespace YandexMusic.Application.Features.Genre.Commands;

public record DeleteGenreCommand(Guid id) : IRequest<bool>;

