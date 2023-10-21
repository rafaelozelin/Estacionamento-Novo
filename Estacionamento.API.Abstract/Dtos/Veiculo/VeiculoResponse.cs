namespace Estacionamento.API.Abstract.Dtos.Veiculo
{
    public class VeiculoResponse
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string TipoVeiculo { get; set; }
        public DateTimeOffset Entrada { get; set; }
        public DateTimeOffset? Saida { get; set; }
    }
}
