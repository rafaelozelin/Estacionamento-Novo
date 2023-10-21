using Estacionamento.API.Abstract.Dtos.Vaga;

namespace Estacionamento.API.Abstract.Services
{
    public interface IVagaService
    {
        public Task Cadastrar(List<VagaRequest> vagaRequest);
        public Task<VagaResponse> Consultar();
        public Task<int> ConsultarVagasRestantes();
        public Task<int> ConsultarTotalVagas();
        public Task<List<VagaVeiculoResponse>> ListarVeiculos();
        public Task<List<VagasDetalhada>> ConsultarSimples();
    }
}
