using AutoMapper;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Web.ViewModels;
using System;

namespace Seds.PMAS.Web.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<RecursoEntity, RecursoViewModel>();
            Mapper.CreateMap<PrefeitoEntity, PrefeitoViewModel>().ForMember(dest => dest.TerminoMandato, opt => opt.MapFrom(src => src.TerminoMandato.Date.ToString()))
                .ForMember(dest => dest.InicioMandato, opt => opt.MapFrom(src => src.InicioMandato.Date.ToString()))
                .ForMember(dest => dest.DataEmissao, opt => opt.MapFrom(src => src.DataEmissao.Value.Date.ToString()));
        }
    }
}