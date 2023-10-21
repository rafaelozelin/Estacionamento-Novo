using Estacionamento.Web.Models;
using Estacionamento.Web.Models.Enums;
using Estacionamento.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Estacionamento.Web.Controllers
{
    public class MotoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpCustomService _httpCustomService;

        public MotoController(IConfiguration configuration, IHttpCustomService httpCustomService)
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
            var urlRedirecionamento = $@"{baseUrl}listar/0";

            List<VeiculoViewModel> motos = null;

            try
            { 
                HttpResponseMessage response = await _httpCustomService.RequestGetAsync(urlRedirecionamento);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    motos = JsonConvert.DeserializeObject<List<VeiculoViewModel>>(content)!;
                }
                else
                {
                    ModelState.AddModelError(null, "Errroooo");
                }

                return View(motos);
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
        public async Task<IActionResult> Cadastrar(VeiculoCadastrar moto)
        {
            try
            {
                var baseUrl = _configuration["Estacionamento.API:BaseUrlVeiculo"];

                moto.TipoVeiculo = TipoVeiculo.Moto;

                var response = await _httpCustomService.RequestPostAsync(baseUrl, moto);

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

        //[HttpGet]
        //public async Task<IActionResult> Editar(int id)
        //{
        //    var baseUrl = _configuration["Estacionamento.API:BaseUrlVeiculo"]; 
        //    var urlRedirecionamento = $@"{baseUrl}listar/{id}";

        //    VeiculoViewModel moto = null;

        //    try
        //    {
        //        HttpResponseMessage response = await _httpCustomService.RequestGetAsync(urlRedirecionamento);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            moto = JsonConvert.DeserializeObject<VeiculoViewModel>(content)!;
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(null, "Errroooo");
        //        }

        //        return View(moto);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        throw ex;
        //    }
        //}

        //[HttpPost]
        //public IActionResult Editar([Bind("Id, Placa, Entrada, Saida")] VeiculoViewModel veiculo)
        //{
        //    //var ve = _veiculos.FirstOrDefault(v => v.Id.Equals(veiculo.Id));

        //    return RedirectToAction("Listar");
        //}

        public async Task<IActionResult> Deletar(int id)
        {
            var baseUrl = _configuration["Estacionamento.API:BaseUrlVeiculo"];
            var urlRedirecionamento = $@"{baseUrl}{id}";

            VeiculoViewModel moto = null;

            try
            {
                HttpResponseMessage response = await _httpCustomService.RequestGetAsync(urlRedirecionamento);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    moto = JsonConvert.DeserializeObject<VeiculoViewModel>(content)!;
                }
                else
                {
                    ModelState.AddModelError(null, "Errroooo");
                }

                return View(moto);
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

