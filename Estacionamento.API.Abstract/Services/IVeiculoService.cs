using Estacionamento.API.Abstract.Dtos.Veiculo;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Abstract.Services
{
    public interface IVeiculoService
    {
        public TipoVeiculo TipoVeiculo { get; }

        Task CadastrarAsync(VeiculoRequest veiculoRequest);
        Task AtualizarAsync(VeiculoRequest veiculoRequest, int id);
        Task DeletarAsync(int id);
        Task<VeiculoResponse> GetById(int id);
        Task<List<VeiculoResponse>> ListarAsync();
    }
}
