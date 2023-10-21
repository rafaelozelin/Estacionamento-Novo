using Estacionamento.API.Domain.Enums;

namespace Estacionamento.API.Abstract.Dtos.Vaga
{
    public class VagaRequest
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public TipoVaga TipoVaga { get; set; } 
    }
}
