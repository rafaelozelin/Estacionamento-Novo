
using Estacionamento.API.Domain.Entities;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Abstract.Repositories
{
    public interface IVeiculoRepository
    {
        public Task CadastrarVeiculoAsync(Veiculo veiculo);
        public Task AtulizarVeiculoAsync(Veiculo veiculo);
        public Task DeletarVeiculo(Veiculo veiculo);
        public Task<Veiculo> GetById(int id);
        public Task<List<Veiculo>> GetByEntrada(DateTime entrada);
        public Task<List<Veiculo>> GetByTipo(TipoVeiculo tipoVeiculo);
        
    }
}
