using AutoMapper;
using Estacionamento.API.Abstract.Dtos.Veiculo;
using Estacionamento.API.Abstract.Extensions;
using Estacionamento.API.Domain.Entities;

namespace Estacionamento.API.Application.Mapping
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {
            CreateMap<Veiculo, VeiculoRequest>();
            CreateMap<Veiculo, VeiculoResponse>()
                .ForMember(dto => dto.TipoVeiculo, opts => opts.MapFrom(domain => domain.TipoVeiculo.GetDescription()));

            CreateMap<VeiculoRequest, Veiculo>();
            CreateMap<VeiculoResponse, Veiculo>();
        }
    }
}
