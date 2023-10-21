using AutoMapper;
using Estacionamento.API.Abstract.Dtos.Vaga;
using Estacionamento.API.Abstract.Dtos.Veiculo;
using Estacionamento.API.Abstract.Extensions;
using Estacionamento.API.Abstract.Repositories;
using Estacionamento.API.Abstract.Services;
using Estacionamento.API.Domain.Entities;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Application.Services
{
    public class VagaService : IVagaService
    {
        private readonly IVagaRepository _vagaRepository;
        private readonly IMapper _mapper;

        public VagaService(IMapper mapper, IVagaRepository vagaRepository)
        {
            _mapper = mapper;
            _vagaRepository = vagaRepository;
        }

        public async Task Cadastrar(List<VagaRequest> vagaRequest)
        { 
            var vagas = vagaRequest.Select(v => _mapper.Map<Vaga>(v)).ToList();

            await _vagaRepository.Cadastrar(vagas);
        }

        public async Task<List<VagasDetalhada>> ConsultarSimples()
        {
            var vagas = await _vagaRepository.Consultar();

            var result = vagas.Select(vaga => new VagasDetalhada
            {
                TipoVaga = vaga.TipoVaga.GetDescription(),
                QuantidadeTotal = vaga.Quantidade,
                QuantidadeRestante = vaga.Quantidade - vaga.Veiculos.Where(ve => ve.Saida is null).Count(),
                Status = VerificarStatus(vaga).GetDescription()
            }).ToList();

            return result;
        }

        public async Task<VagaResponse> Consultar()
        {
            var vagas = await _vagaRepository.Consultar();

            var result = new VagaResponse
            {
                TotalVagas = vagas.Sum(v => v.Quantidade),
                TotalVagasRestante = VerificarVagasRestante(vagas),               
                VagasDetalhada = vagas.Select(vaga => new VagasDetalhada
                {
                    TipoVaga = vaga.TipoVaga.GetDescription(),
                    QuantidadeTotal = vaga.Quantidade,
                    QuantidadeRestante = vaga.Quantidade - vaga.Veiculos.Where(ve => ve.Saida is null).Count(),
                    Status = VerificarStatus(vaga).GetDescription()
                }).ToList()
            };

            result.StatusEstacionamento = VerificarStatusEstacionamento(result.TotalVagas, result.TotalVagasRestante).GetDescription();

            return result;
        }

        public async Task<int> ConsultarVagasRestantes()
        {
            var vagas = await _vagaRepository.Consultar();

            return VerificarVagasRestante(vagas);
        }

        public async Task<int> ConsultarTotalVagas()
        {
            var vagas = await _vagaRepository.Consultar();

            return vagas.Sum(v => v.Quantidade);
        }

        public async Task<List<VagaVeiculoResponse>> ListarVeiculos()
        {
            var vagasVeiculos = await _vagaRepository.Consultar();

            var result = vagasVeiculos.Select(vaga => new VagaVeiculoResponse()
            {
                TipoVaga = vaga.TipoVaga.GetDescription(),
                Quantidade = vaga.Quantidade,
                Veiculos = vaga.Veiculos.Select(c => _mapper.Map<VeiculoResponse>(c)).ToList()
            }).ToList();

            return result;
        }
        

        private static int VerificarVagasRestante(List<Vaga> vagas)
        {
            return vagas.Sum(v => v.Quantidade) - vagas.SelectMany(va => va.Veiculos.Where(ve => ve.Saida is null)).Count();
        }

        private static StatusEstacionamento VerificarStatusEstacionamento(int total, int restante)
        {
            if (total == restante)
                return StatusEstacionamento.Vazio;
            else if (restante == 0)
                return StatusEstacionamento.Cheio;

            return StatusEstacionamento.HaVagas;
        }

        private static StatusEstacionamento VerificarStatus(Vaga vaga)
        {
            var quantidadeRestante = vaga.Veiculos.Where(ve => ve.Saida is null).Count();
            var quantidadeTotal = vaga.Quantidade;

            if (quantidadeTotal == quantidadeRestante)
                return StatusEstacionamento.Cheio;
            else if (quantidadeRestante == 0)
                return StatusEstacionamento.Vazio;

            return StatusEstacionamento.HaVagas;
        }
    }
}
