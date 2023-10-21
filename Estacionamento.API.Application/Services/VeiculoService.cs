using AutoMapper;
using Estacionamento.API.Abstract.Dtos.Veiculo;
using Estacionamento.API.Abstract.Repositories;
using Estacionamento.API.Abstract.Services;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Application.Services
{
    public abstract class VeiculoService : IVeiculoService
    {
        public abstract TipoVeiculo TipoVeiculo { get; }

        protected readonly IVeiculoRepository _veiculoRepository;
        private readonly IMapper _mapper;

        protected VeiculoService(IVeiculoRepository veiculoRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _mapper = mapper;
        }

        public abstract Task CadastrarAsync(VeiculoRequest veiculoRequest);
        public abstract Task AtualizarAsync(VeiculoRequest veiculoRequest, int id);
        public abstract Task<List<VeiculoResponse>> ListarAsync();
        public async Task<VeiculoResponse> GetById(int id)
        {
            var veiculo = await _veiculoRepository.GetById(id);

            if (veiculo is null)
                throw new Exception("Veículo não encontrado");

            return _mapper.Map<VeiculoResponse>(veiculo);
        }
        public async Task DeletarAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetById(id);

            if (veiculo is null)
                throw new Exception("Veículo não encontrado");

            veiculo.Saida = DateTime.Now;

            await _veiculoRepository.AtulizarVeiculoAsync(veiculo);
        }
    }
}
