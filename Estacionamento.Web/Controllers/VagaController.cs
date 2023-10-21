using Estacionamento.Web.Models;
using Estacionamento.Web.Models.Enums;
using Estacionamento.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Estacionamento.Web.Controllers
{
    public class VagaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpCustomService _httpCustomService;

        public VagaController(IConfiguration configuration, IHttpCustomService httpCustomService)
        {
            _configuration = configuration;
            _httpCustomService = httpCustomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var baseUrl = _configuration["Estacionamento.API:BaseUrlVaga"];
            var urlRedirecionamento = $@"{baseUrl}consulta-simples";

            List<VagaListaViewModel> vagas = null;

            try
            {
                HttpResponseMessage response = await _httpCustomService.RequestGetAsync(urlRedirecionamento);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    vagas = JsonConvert.DeserializeObject<List<VagaListaViewModel>>(content)!;
                }
                else
                {
                    ModelState.AddModelError(null, "Errroooo");
                }

                return View(vagas);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                ViewBag.Error = message;
                return View();
            }
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(VagaViewModel vaga)
        {
            var baseUrl = _configuration["Estacionamento.API:BaseUrlVaga"];

            try
            {
                var vagas = new List<VagaCadastrar>
                {
                    new VagaCadastrar()
                    {
                        TipoVaga = TipoVaga.Moto,
                        Quantidade = vaga.QuantidadeMoto
                    },
                    new VagaCadastrar()
                    {
                        TipoVaga = TipoVaga.Carro,
                        Quantidade = vaga.QuantidadeMoto
                    },new VagaCadastrar()
                    {
                        TipoVaga = TipoVaga.Grande,
                        Quantidade = vaga.QuantidadeGrande
                    },
                };

                var response = await _httpCustomService.RequestPostAsync(baseUrl, vagas);

                var responseRequest = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Errroooo");

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
