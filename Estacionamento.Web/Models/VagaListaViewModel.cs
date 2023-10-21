using Estacionamento.Web.Models.Enums;

namespace Estacionamento.Web.Models
{
    public class VagaListaViewModel
    {
        public string TipoVaga { get; set; }
        public string QuantidadeTotal { get; set; }
        public string QuantidadeRestante { get; set; }
        public string Status { get; set; }
    }
}
