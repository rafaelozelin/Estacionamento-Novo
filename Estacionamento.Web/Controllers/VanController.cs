using Estacionamento.Web.Models;
using Estacionamento.Web.Models.Enums;
using Estacionamento.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Estacionamento.Web.Controllers
{
    public class VanController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpCustomService _httpCustomService;

        public VanController(IConfiguration configuration, IHttpCustomService httpCustomService)
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
            var baseUrl = _configuration["Estacionamento.API:BaseUrlVeiculo"];
            var urlRedirecionamento = $@"{baseUrl}listar/2";

            List<VeiculoViewModel> vans = null;

            try
            {
                HttpResponseMessage response = await _httpCustomService.RequestGetAsync(urlRedirecionamento);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    vans = JsonConvert.DeserializeObject<List<VeiculoViewModel>>(content)!;
                }
                else
                {
                    ModelState.AddModelError(null, "Errroooo");
                }

                return View(vans);
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
        public async Task<IActionResult> Cadastrar(VeiculoCadastrar van)
        {
            try
            {
                var baseUrl = _configuration["Estacionamento.API:BaseUrlVeiculo"];

                van.TipoVeiculo = TipoVeiculo.Van;

                var response = await _httpCustomService.RequestPostAsync(baseUrl, van);

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

        public async Task<IActionResult> Deletar(int id)
        {
            var baseUrl = _configuration["Estacionamento.API:BaseUrlVeiculo"];
            var urlRedirecionamento = $@"{baseUrl}{id}";

            VeiculoViewModel van = null;

            try
            {
                HttpResponseMessage response = await _httpCustomService.RequestGetAsync(urlRedirecionamento);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    van = JsonConvert.DeserializeObject<VeiculoViewModel>(content)!;
                }
                else
                {
                    ModelState.AddModelError(null, "Errroooo");
                }

                return View(van);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(string id)
        {
            var baseUrl = _configuration["Estacionamento.API:BaseUrlVeiculo"];
            var urlRedirecionamento = $@"{baseUrl}{id}";

            try
            {
                var response = await _httpCustomService.RequestDeleteAsync(urlRedirecionamento);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    ModelState.AddModelError(null, "Errroooo");
                }

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw ex;
            }
        }
    }
}

