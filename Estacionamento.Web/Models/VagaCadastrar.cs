using Estacionamento.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Web.Models
{
    public class VeiculoCadastrar
    {
        [Required(ErrorMessage = "O campo Placa é obrigatório.")]
        public string Placa { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
    }
}
