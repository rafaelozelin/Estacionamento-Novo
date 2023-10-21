using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Abstract.Dtos.Veiculo
{
    public class VeiculoRequest
    {
        public int? Id { get; set; }
        public string? Placa { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
        public DateTime? Entrada { get; set; }
        public DateTime? Saida { get; set; }
    }
}
