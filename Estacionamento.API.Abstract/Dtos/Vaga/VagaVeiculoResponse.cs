using Estacionamento.API.Abstract.Dtos.Veiculo;

namespace Estacionamento.API.Abstract.Dtos.Vaga
{
    public class VagaVeiculoResponse
    {
        public int Quantidade { get; set; }
        public string TipoVaga { get; set; }
        public List<VeiculoResponse> Veiculos { get; set; }
    }
}
