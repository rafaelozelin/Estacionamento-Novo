using Estacionamento.API.Domain.Entities;
using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Abstract.Repositories
{
    public interface IVagaRepository
    {
        public Task Cadastrar(List<Vaga> vagas);
        public Task<List<Vaga>> Consultar();
        public Task<List<Vaga>> ConsultarPorTipos(List<TipoVaga> tipoVaga);
    }
}
