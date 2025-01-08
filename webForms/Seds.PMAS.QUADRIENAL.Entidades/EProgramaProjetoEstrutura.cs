using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class ValidacaoProgramaProjetoEstrutura
    {
        public ValidacaoProgramaProjetoEstrutura()
        {
            this.ValidarEstruturaTipoPrograma = false;
            this.ValidarEstruturaAderenciaAcessuas = false;
            this.ValidarEstruturaExecutaPrograma = false;
            this.ValidarEstruturaDataProgramaProjeto = false;
            this.ValidarEstruturaDataAdesao = false;
            this.ValidarEstruturaInterlecutorMunicipal = false;
            this.ValidarEstruturaPrevisaoAnual = false;
            this.ValidarEstruturaMetaPactuada = false;
            this.ValidarEstruturaConstrucaoUnidadesIdoso = false;
            this.ValidarEstruturaCaracterizacaoUsuarios = false;
            this.ValidarEstruturaAcoesDesenvolvidas = false;
            this.ValidarEstruturaSeloAmigoIdoso = false;
            this.ValidarEstruturaAtividadesRealizadas = false;
            this.ValidarEstruturaAbrangencia = false;
            this.ValidarEstruturaParcerias = false;
            this.ValidarEstruturaRecursosFinanceiros = false;
            this.ValidarEstruturaRecursosFinanceirosAbas = false;
        }
        public bool ValidarEstruturaTipoPrograma { get; set;}
        public bool ValidarEstruturaAderenciaAcessuas { get; set;}
        public bool ValidarEstruturaExecutaPrograma { get; set;}
        public bool ValidarEstruturaDataProgramaProjeto { get; set;}
        public bool ValidarEstruturaDataAdesao { get; set;}
        public bool ValidarEstruturaInterlecutorMunicipal { get; set;}
        public bool ValidarEstruturaPrevisaoAnual { get; set;}
        public bool ValidarEstruturaMetaPactuada { get; set;}
        public bool ValidarEstruturaConstrucaoUnidadesIdoso { get; set;}
        public bool ValidarEstruturaCaracterizacaoUsuarios { get; set;}
        public bool ValidarEstruturaAcoesDesenvolvidas { get; set;}
        public bool ValidarEstruturaSeloAmigoIdoso { get; set;}
        public bool ValidarEstruturaAtividadesRealizadas { get; set;}
        public bool ValidarEstruturaAbrangencia { get; set;}
        public bool ValidarEstruturaParcerias { get; set;}
        public bool ValidarEstruturaRecursosFinanceiros { get; set;}
        public bool ValidarEstruturaRecursosFinanceirosAbas { get; set;}

        public void Clear()
        {
            this.ValidarEstruturaTipoPrograma = false;
            this.ValidarEstruturaAderenciaAcessuas  = false;
            this.ValidarEstruturaExecutaPrograma  = false;
            this.ValidarEstruturaDataProgramaProjeto  = false;
            this.ValidarEstruturaDataAdesao  = false;
            this.ValidarEstruturaInterlecutorMunicipal  = false;
            this.ValidarEstruturaPrevisaoAnual  = false;
            this.ValidarEstruturaMetaPactuada  = false;
            this.ValidarEstruturaConstrucaoUnidadesIdoso  = false;
            this.ValidarEstruturaCaracterizacaoUsuarios  = false;
            this.ValidarEstruturaAcoesDesenvolvidas  = false;
            this.ValidarEstruturaSeloAmigoIdoso  = false;
            this.ValidarEstruturaAtividadesRealizadas  = false;
            this.ValidarEstruturaAbrangencia  = false;
            this.ValidarEstruturaParcerias  = false;
            this.ValidarEstruturaRecursosFinanceiros  = false;
            this.ValidarEstruturaRecursosFinanceirosAbas  = false;
        }

    }
}
