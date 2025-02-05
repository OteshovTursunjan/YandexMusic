using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using MediatR;
using YandexMusic.Application.Features.Tarrif.Commands;
using YandexMusic.Application.Features.Tarrif.Queries;

namespace YandexMusic.Controllers.user
{
    public class TarrifTypeController : Controller
    {
       public readonly IMediator mediator;
        public TarrifTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Create-Tarrif")]
        public async Task<IActionResult> AddTarrif(TarrifTypeDTO tarrifTypeDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newTarrif = await mediator.Send(new CreateTarrifCommand(tarrifTypeDTO));
            return  newTarrif == null ? NotFound() : Ok(newTarrif);
        }
        [HttpPut("UpdateTarrif{id}")]
        public async Task<IActionResult> UpdateTarrif([FromRoute] Guid id, [FromBody] TarrifTypeDTO tarrif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await mediator.Send(new UpdateTarrifCommand(id, tarrif)); 
            return Ok(res);
        }
        [HttpGet("GetTarrif{id}")]
        public async Task<IActionResult> GetTarrif([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newTarrif = await mediator.Send(new GetTarrifByIdQueries(id));

            return newTarrif == null ? NotFound() : Ok(newTarrif);
        }
        [HttpDelete("DeleteTarrif/{id}")]
        public async Task<IActionResult> DeleteTarrif([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newTarruf = await mediator.Send(new DeleteTarrifCommand(id)) ;

            return newTarruf == null ? NotFound() : Ok(newTarruf);
        }
    }
}
