using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Domain.Entities
{
    public class Veiculo : BaseEntity
    {
        public string Placa { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
        public int IdVaga { get; set; }
        public virtual Vaga Vaga { get; set; }
    }
}
