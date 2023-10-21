using Estacionamento.API.Abstract.Factories;
using Estacionamento.API.Abstract.Services;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Application.Factory
{
    public class VeiculoServiceFactory : IVeiculoServiceFactory
    {
        private readonly Func<IEnumerable<IVeiculoService>> _collectionService;

        public VeiculoServiceFactory(Func<IEnumerable<IVeiculoService>> collectionService)
        {
            _collectionService = collectionService;
        }

        public IVeiculoService Create(TipoVeiculo tipoVeiculo)
        {
            var service = _collectionService().FirstOrDefault(c => c.TipoVeiculo == tipoVeiculo);

            return service ?? throw new Exception("Não encontramos esse tipo de veículo");
        }
    }
}
