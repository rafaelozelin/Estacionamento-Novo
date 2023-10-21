using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Web.Models
{
    public class VeiculoViewModel
    {
        public int? Id { get; set; }
        public string Placa { get; set; }
        public string? TipoVeiculo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? Entrada { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? Saida { get; set; }
    }
}
