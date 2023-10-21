﻿using AutoMapper;
using Estacionamento.API.Abstract.Dtos.Veiculo;
using Estacionamento.API.Abstract.Repositories;
using Estacionamento.API.Domain.Entities;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Application.Services
{
    public class CarroService : VeiculoService
    {
        private readonly IVagaRepository _vagaRepository;
        private readonly IMapper _map;

        public override TipoVeiculo TipoVeiculo => TipoVeiculo.Carro;

        public CarroService(IVeiculoRepository veiculoRepository, 
                            IMapper mapper,
                            IMapper map,
                            IVagaRepository vagaRepository) : base(veiculoRepository, mapper)
        {
            _vagaRepository = vagaRepository;
            _map = map;
        }

        public override async Task CadastrarAsync(VeiculoRequest veiculoRequest)
        {
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Carro, TipoVaga.Grande };

            var vagas = await _vagaRepository.ConsultarPorTipos(tipoVagas);

            if (!vagas.Any())
                throw new Exception("Solicitar criação do estacionamento de carros");

            var temVaga = VerificaVagas(vagas);

            if (!temVaga.Item1)
                throw new Exception("Não há vagas para carros");

            var veiculo = MontarVeiculo(veiculoRequest, temVaga.Item2);

            await _veiculoRepository.CadastrarVeiculoAsync(veiculo);
        }

        public override async Task AtualizarAsync(VeiculoRequest veiculoRequest, int id)
        {
            if (veiculoRequest.Id != id)
                throw new Exception("Carro não encontrado");

            var veiculo = await _veiculoRepository.GetById(id);

            if (veiculo is null)
                throw new Exception("Carro não encontrado");

            veiculo.Placa = veiculoRequest.Placa!;
            veiculo.Saida = DateTime.Now;

            await _veiculoRepository.AtulizarVeiculoAsync(veiculo);
        }

        public override async Task<List<VeiculoResponse>> ListarAsync()
        {
            var veiculos = await _veiculoRepository.GetByTipo(TipoVeiculo.Carro);

            return veiculos.Select(c => _map.Map<VeiculoResponse>(c)).ToList();
        }

        private Veiculo MontarVeiculo(VeiculoRequest veiculoRequest, int idVaga)
        {
            var veiculo = _map.Map<Veiculo>(veiculoRequest);

            veiculo.Entrada = DateTime.Now;
            veiculo.TipoVeiculo = TipoVeiculo.Carro;
            veiculo.IdVaga = idVaga;

            return veiculo;
        }

        private static (bool, int) VerificaVagas(List<Vaga> vagas)
        {
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Carro, TipoVaga.Grande };
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
