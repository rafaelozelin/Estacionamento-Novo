using Estacionamento.API.Abstract.Repositories;
using Estacionamento.API.Domain.Entities;
using Estacionamento.API.Domain.Enums;
using Estacionamento.API.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infra.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly DataContext _context;

        public VeiculoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CadastrarVeiculoAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task AtulizarVeiculoAsync(Veiculo veiculo)
        {
            _context.Entry(veiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletarVeiculo(Veiculo veiculo)
        {
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<Veiculo> GetById(int id)
        {
            var veiculo = await _context.Veiculos
                .SingleOrDefaultAsync(c => c.Id == id);

            return veiculo;
        }

        public async Task<List<Veiculo>> GetByEntrada(DateTime entrada)
        {
            var veiculos = await _context.Veiculos
                .Where(c => c.Entrada == entrada).ToListAsync();

            return veiculos;
        }

        public async Task<List<Veiculo>> GetByTipo(TipoVeiculo tipoVeiculo)
        {
            var veiculos = await _context.Veiculos
                .Where(v => v.TipoVeiculo == tipoVeiculo)
                .OrderBy(v => v.Saida != null)
                .ToListAsync();

            return veiculos;
        }
    }
}
