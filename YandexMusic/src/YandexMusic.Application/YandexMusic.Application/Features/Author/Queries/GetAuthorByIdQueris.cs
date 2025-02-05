using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using YandexMusic.DataAccess.DTOs;
namespace YandexMusic.Application.Features.Author.Queries;

public  record GetAuthorByIdQueris(Guid id) : IRequest<AuthorDTO>;


