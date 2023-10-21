using System.ComponentModel;
using System.Runtime.Serialization;

namespace Estacionamento.Web.Models.Enums
{
    public enum TipoVaga
    {
        [EnumMember]
        [Description("Moto")]
        Moto = 0,

        [EnumMember]
        [Description("Carro")]
        Carro = 1,

        [EnumMember]
        [Description("Grande")]
        Grande = 2,
    }
}
