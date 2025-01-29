using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;

namespace YandexMusic.Controllers.user
{
    public class TarrifTypeController : Controller
    {
        public readonly ITarrifTypeService tarrifTypeService;
        public TarrifTypeController(ITarrifTypeService tarrifTypeService)
        {
            this.tarrifTypeService = tarrifTypeService;
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

            var newTarrif = await tarrifTypeService.AddTarrifAsync(tarrifTypeDTO);
            return  newTarrif == null ? NotFound() : Ok(newTarrif);
        }
        [HttpPut("UpdateTarrif")]
        public async Task<IActionResult> UpdateTarrif([FromRoute] Guid id, [FromBody] TarrifTypeDTO tarrif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await tarrifTypeService.UpdateTarrifAsync(id, tarrif);
            return Ok(res);
        }
        [HttpGet("GetTarrif{id}")]
        public async Task<IActionResult> GetTarrif([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newTarrif = await tarrifTypeService.GetByIdAsync(id);

            return newTarrif == null ? NotFound() : Ok(newTarrif);
        }
        [HttpDelete("DeleteTarrif/{id}")]
        public async Task<IActionResult> DeleteTarrif([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newTarruf = tarrifTypeService.DeleteTarrifAsync(id);

            return newTarruf == null ? NotFound() : Ok(newTarruf);
        }
    }
}
