using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.Application.Features.Author.Commands;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Features.Author.Handler
{
    public  class DeleteAuthorHadler : IRequestHandler<DeleteAuthorCommand,bool>
    {
        private readonly IAuthorRepository authorRepository;
        public DeleteAuthorHadler(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
           var auhtor = await authorRepository.GetFirstAsync(u => u.Id == request.id);
            if (auhtor == null) return false;
            
            await authorRepository.DeleteAsync(auhtor);
            return true;
        }
    }
}
