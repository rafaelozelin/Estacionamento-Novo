using Estacionamento.API.Abstract.Services;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Abstract.Factories
{
    public interface IVeiculoServiceFactory
    {
        public IVeiculoService Create(TipoVeiculo tipoVeiculo);
    }
}
