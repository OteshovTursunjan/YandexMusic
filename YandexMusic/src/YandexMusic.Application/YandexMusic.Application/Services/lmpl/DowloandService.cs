using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.Repository;

namespace YandexMusic.Application.Services.lmpl
{
    public  class DowloandService : IDowloandService
    {
        public readonly IDowloandRepository _dowloandRepository;
        public readonly IMusicRepository _musicRepository;
        public DowloandService(IDowloandRepository dowloandRepository, IMusicRepository musicRepository)
        {
            _dowloandRepository = dowloandRepository;
            _musicRepository = musicRepository;
        }
       
    }
}
