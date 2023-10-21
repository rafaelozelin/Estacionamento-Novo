using AutoMapper;
using Estacionamento.API.Abstract.Dtos.Veiculo;
using Estacionamento.API.Abstract.Repositories;
using Estacionamento.API.Domain.Entities;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Application.Services
{
    public class MotoService : VeiculoService
    {
        private readonly IVagaRepository _vagaRepository;

        public override TipoVeiculo TipoVeiculo => TipoVeiculo.Moto;

        public MotoService(IVeiculoRepository veiculoRepository,
                            IMapper mapper,
                            IVagaRepository vagaRepository) : base(veiculoRepository, mapper)
        {
            _vagaRepository = vagaRepository;
        }

        public override async Task CadastrarAsync(VeiculoRequest veiculoRequest)
        {
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Moto, TipoVaga.Carro, TipoVaga.Grande };

            var vagas = await _vagaRepository.ConsultarPorTipos(tipoVagas);

            if (!vagas.Any())
                throw new Exception("Solicitar criação do estacionamento de motos");

            var temVaga = VerificaVagas(vagas);

            if (!temVaga.Item1)
                throw new Exception("Não há vagas para motos");

            var veiculo = MontarVeiculo(veiculoRequest, temVaga.Item2);

            await _veiculoRepository.CadastrarVeiculoAsync(veiculo);
        }

        public override async Task AtualizarAsync(VeiculoRequest veiculoRequest, int id)
        {
            if (veiculoRequest.Id != id)
                throw new Exception("Moto não encontrada");

            var veiculo = await _veiculoRepository.GetById(id);

            if (veiculo is null)
                throw new Exception("Moto não encontrada");

            veiculo.Placa = veiculoRequest.Placa!;
            veiculo.Saida = DateTime.Now;

            await _veiculoRepository.AtulizarVeiculoAsync(veiculo);
        }

        public override async Task<List<VeiculoResponse>> ListarAsync()
        {
            var veiculos = await _veiculoRepository.GetByTipo(TipoVeiculo.Moto);

            return veiculos.Select(c => _mapper.Map<VeiculoResponse>(c)).ToList();
        }

        private Veiculo MontarVeiculo(VeiculoRequest veiculoRequest, int idVaga)
        {
            var veiculo = _mapper.Map<Veiculo>(veiculoRequest);

            veiculo.Entrada = DateTime.Now;
            veiculo.TipoVeiculo = TipoVeiculo.Moto;
            veiculo.IdVaga = idVaga;

            return veiculo;
        }

        private static (bool, int) VerificaVagas(List<Vaga> vagas)
        {
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Moto, TipoVaga.Carro, TipoVaga.Grande };
            var temVaga = false;
            var idVaga = 0;

            foreach (var tipo in tipoVagas)
            {
                var vagaEspecifica = vagas.Where(vaga => vaga.TipoVaga == tipo).First();

                var totalVagas = vagaEspecifica.Quantidade;
                var vagasEmUso = vagaEspecifica.Veiculos.Where(ve => ve.Saida is null).Count();

                if (vagasEmUso < totalVagas)
                {
                    temVaga = true;
                    idVaga = vagaEspecifica.Id;
                    break;
                }
            }

            return (temVaga, idVaga);
        }
    }
}
