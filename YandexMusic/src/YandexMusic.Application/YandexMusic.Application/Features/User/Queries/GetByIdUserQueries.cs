using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Features.User.Queries;

public  record GetByIdUserQueries(Guid id) : IRequest<YandexMusics.Core.Entities.Music.User>;
