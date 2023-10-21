using System.ComponentModel;
using System.Runtime.Serialization;

namespace Estacionamento.Web.Models.Enums
{
    public enum TipoVeiculo
    {
        [EnumMember]
        [Description("Moto")]
        Moto = 0,

        [EnumMember]
        [Description("Carro")]
        Carro = 1,

        [EnumMember]
        [Description("Van")]
        Van = 2,
    }
}
