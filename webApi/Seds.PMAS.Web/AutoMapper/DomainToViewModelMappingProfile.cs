using AutoMapper;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Web.ViewModels;
using System;

namespace Seds.PMAS.Web.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<RecursoViewModel, RecursoEntity>();
            Mapper.CreateMap<PrefeitoViewModel, PrefeitoEntity>().ForMember(dest => dest.TerminoMandato, opt => opt.MapFrom(src => DateTime.Parse(src.TerminoMandato)))
                .ForMember(dest => dest.DataEmissao, opt => opt.MapFrom(src => DateTime.Parse(src.DataEmissao).Date))
                .ForMember(dest => dest.InicioMandato, opt => opt.MapFrom(src => DateTime.Parse(src.InicioMandato).Date));
        }
    }
}