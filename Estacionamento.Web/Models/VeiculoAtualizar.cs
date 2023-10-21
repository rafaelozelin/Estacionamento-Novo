using Estacionamento.Web.Models.Enums;

namespace Estacionamento.Web.Models
{
    public class VeiculoAtualizar
    {
        public int? Id { get; set; }
        public string? Placa { get; set; }
        public TipoVeiculo? TipoVeiculo { get; set; }
        public DateTimeOffset? Entrada { get; set; }
        public DateTimeOffset? Saida { get; set; }
    }
}
