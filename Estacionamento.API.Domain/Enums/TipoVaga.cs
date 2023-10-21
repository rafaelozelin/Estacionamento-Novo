using System.ComponentModel;
using System.Runtime.Serialization;

namespace Estacionamento.API.Domain.Enums
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
