using Estacionamento.API.Abstract.Repositories;
using Estacionamento.API.Domain.Entities;
using Estacionamento.API.Domain.Enums;
using Estacionamento.API.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infra.Repositories
{
    public class VagaRepository : IVagaRepository
    {
        private readonly DataContext _context;

        public VagaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Cadastrar(List<Vaga> vagas)
        {
            _context.Vagas.AddRange(vagas);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vaga>> Consultar()
        {
            var vagas = await _context.Vagas
                .Include(v => v.Veiculos)
                .AsNoTracking()
                .ToListAsync();

            return vagas;
        }

        public async Task<List<Vaga>> ConsultarPorTipos(List<TipoVaga> tipoVagas)
        {
            var vagas = await _context.Vagas
                .Include(v => v.Veiculos)
                .Where(v => tipoVagas.Contains(v.TipoVaga))
                .AsNoTracking()
                .ToListAsync();

            return vagas;
        } 
    }
}
