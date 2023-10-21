using AutoMapper;
using Estacionamento.API.Abstract.Dtos.Vaga;
using Estacionamento.API.Domain.Entities;

namespace Estacionamento.API.Application.Mapping
{
    public class VagaProfile : Profile
    {
        public VagaProfile()
        { 
            CreateMap<Vaga, VagaRequest>();
            CreateMap<Vaga, VagaResponse>();

            CreateMap<VagaRequest, Vaga>();
            CreateMap<VagaResponse, Vaga>();
        }
    }
}
