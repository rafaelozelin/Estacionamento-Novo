using Estacionamento.API.Abstract.Dtos.Vaga;
using Estacionamento.API.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Estacionamento.API.Controllers
{
    [ApiController]
    [Route("api/v1/vaga")]
    public class VagaController : ControllerBase
    {
        protected readonly ILogger<VagaController> _logger;

        public VagaController(ILogger<VagaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromServices] IVagaService vagaService, [FromBody] List<VagaRequest> vagaRequest)
        {
            try
            {
                await vagaService.Cadastrar(vagaRequest);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Algo inesperado ocorreu no cadastro das vagas: {exception.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Algo inesperado ocorreu",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("consulta-simples")]
        public async Task<IActionResult> ConsultarSimples([FromServices] IVagaService vagaService)
        {
            try
            {
                return Ok(await vagaService.ConsultarSimples());
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Algo inesperado ocorreu na consulta: {exception.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Algo inesperado ocorreu",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("consulta-detalhada")]
        public async Task<IActionResult> Consultar([FromServices] IVagaService vagaService)
        {
            try
            {
                return Ok(await vagaService.Consultar());
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Algo inesperado ocorreu na consulta: {exception.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Algo inesperado ocorreu",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("consultar-vagas-restantes")]
        public async Task<IActionResult> ConsultarVagasRestantes([FromServices] IVagaService vagaService)
        {
            try
            {
                return Ok(await vagaService.ConsultarVagasRestantes());
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Algo inesperado ocorreu na consulta restante",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("consultar-total-vagas")]
        public async Task<IActionResult> ConsultarTotalVagas([FromServices] IVagaService vagaService)
        {
            try
            {
                return Ok(await vagaService.ConsultarTotalVagas());
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Algo inesperado ocorreu na consulta todas vagas",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("listar-todos-veiculos")]
        public async Task<IActionResult> ListarVeiculos([FromServices] IVagaService vagaService)
        {
            try
            {
                return Ok(await vagaService.ListarVeiculos());
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Algo inesperado ocorreu listagem de todos veículos",
                      detail = exception.Message
                  });
            }
        }
    }
}
