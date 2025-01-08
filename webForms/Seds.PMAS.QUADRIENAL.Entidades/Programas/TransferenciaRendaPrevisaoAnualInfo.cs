using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Entidades
{
    public class TransferenciaRendaPrevisaoAnualInfo
    {
        public int Id { get; set; }

        public Int32 IdTransferenciaRenda { get; set; }

        public Int32 IdPrefeitura { get; set; }

        public Int32? EstimativaFamilias2017 { get; set; }

        public Int32? EstimativaFamilias2018 { get; set; }

        public Int32? EstimativaFamilias2019 { get; set; }

        public Int32? EstimativaFamilias2020 { get; set; }

        public Int32? EstimativaFamilias2021 { get; set; }

        public Int32? EstimativaFamilias2022 { get; set; }

        public Int32? EstimativaFamilias2023 { get; set; }

        public Int32? EstimativaFamilias2024 { get; set; }

        public Int32? EstimativaFamilias2025 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2017 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2018 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2019 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2020 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2021 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2022 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2023 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2024 { get; set; }

        public Int32? NumeroFamiliasBeneficiarias2025 { get; set; }

        public Int32? FamiliasCadastradas2017 { get; set; }

        public Int32? FamiliasCadastradas2018 { get; set; }

        public Int32? FamiliasCadastradas2019 { get; set; }

        public Int32? FamiliasCadastradas2020 { get; set; }

        public Int32? FamiliasCadastradas2021 { get; set; }

        public Int32? FamiliasCadastradas2022 { get; set; }

        public Int32? FamiliasCadastradas2023 { get; set; }

        public Int32? FamiliasCadastradas2024 { get; set; }

        public Int32? FamiliasCadastradas2025 { get; set; }

        public Decimal? RepasseMensal2017 { get; set; }

        public Decimal? RepasseMensal2018 { get; set; }

        public Decimal? RepasseMensal2019 { get; set; }

        public Decimal? RepasseMensal2020 { get; set; }

        public Decimal? RepasseMensal2021 { get; set; }

        public Decimal? RepasseMensal2022 { get; set; }

        public Decimal? RepasseMensal2023 { get; set; }

        public Decimal? RepasseMensal2024 { get; set; }

        public Decimal? RepasseMensal2025 { get; set; }

        public Int32? MetaPactuada2017 { get; set; }

        public Int32? MetaPactuada2018 { get; set; }

        public Int32? MetaPactuada2019 { get; set; }

        public Int32? MetaPactuada2020 { get; set; }

        public Int32? MetaPactuada2021 { get; set; }

        public Int32? MetaPactuada2022 { get; set; }

        public Int32? MetaPactuada2023 { get; set; }

        public Int32? MetaPactuada2024 { get; set; }

        public Int32? MetaPactuada2025 { get; set; }

        public Int32? NumeroAtendidos2021 { get; set; }

        public Int32? NumeroAtendidos2022 { get; set; }

        public Int32? NumeroAtendidos2023 { get; set; }

        public Int32? NumeroAtendidos2024 { get; set; }

        public Int32? NumeroAtendidos2025 { get; set; }

        public Decimal? ValorRepasseEstadual2021 { get; set; }

        public Decimal? ValorRepasseEstadual2022 { get; set; }

        public Decimal? ValorRepasseEstadual2023 { get; set; }

        public Decimal? ValorRepasseEstadual2024 { get; set; }

        public Decimal? ValorRepasseEstadual2025 { get; set; }


        public Decimal? ValorReprogramacaoRepasseEstadual2021 { get; set; }

        public Decimal? ValorReprogramacaoRepasseEstadual2022 { get; set; }

        public Decimal? ValorReprogramacaoRepasseEstadual2023 { get; set; }

        public Decimal? ValorReprogramacaoRepasseEstadual2024 { get; set; }

        public Decimal? ValorReprogramacaoRepasseEstadual2025 { get; set; }


        public Int32? AuxilioAluguelNumeroAtendidasExercicio2024 { get; set; }

        public Int32? AuxilioAluguelAtivasExercicio2024 { get; set; }

        public Int32? AuxilioAluguelRecebidasExercicio2024 { get; set; }

        public Int32? AuxilioAluguelNumeroAtendidasExercicio2025 { get; set; }

        public Int32? AuxilioAluguelAtivasExercicio2025 { get; set; }

        public Int32? AuxilioAluguelRecebidasExercicio2025 { get; set; }


        //substituir MetaPactuada por Media Mensal 

        public Decimal? CalculoAcaoRendaPrevisaoAnualSistemaAnterior { get { return EstimativaFamilias2021.HasValue ? 0 * 80 * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoAcaoRendaPrevisaoAnualExercicio1 { get { return EstimativaFamilias2021.HasValue ? EstimativaFamilias2021.Value * 80 * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoAcaoRendaPrevisaoAnualExercicio2 { get { return EstimativaFamilias2022.HasValue ? EstimativaFamilias2022.Value * 80 * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoAcaoRendaPrevisaoAnualExercicio3 { get { return EstimativaFamilias2023.HasValue ? EstimativaFamilias2023.Value * 80 * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoAcaoRendaPrevisaoAnualExercicio4 { get { return EstimativaFamilias2024.HasValue ? EstimativaFamilias2024.Value * 80 * 12 : new Nullable<Decimal>(); } }


        int salarioMinimoSistemaAnterior = 1100;//937; //Ref 2017

        int salarioMinimoExercicio1 = 1100;  //954;   // 2018
        int salarioMinimoExercicio2 = 1320; //998;   // 2019
        //int salarioMinimoExercicio3 = 1100; //1039;  // 2020
        int salarioMinimo2Exercicio3 = 1320; //1045; //2020 2° ajuste
        int salarioMinimoExercicio4 = 1100;  //Será novo em 2021

        public Decimal? CalculoBPCPrevisaoAnualSistemaAnterior { get { return MetaPactuada2021.HasValue ? MetaPactuada2021.Value * salarioMinimoSistemaAnterior * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoBPCPrevisaoAnualExercicio1 { get { return MetaPactuada2022.HasValue ? MetaPactuada2022.Value * salarioMinimoExercicio1 * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoBPCPrevisaoAnualExercicio2 { get { return MetaPactuada2023.HasValue ? MetaPactuada2023.Value * salarioMinimoExercicio2 * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoBPCPrevisaoAnualExercicio3 { get { return MetaPactuada2024.HasValue ? MetaPactuada2024.Value * salarioMinimo2Exercicio3 * 12 : new Nullable<Decimal>(); } }

        public Decimal? CalculoBPCPrevisaoAnualExercicio4 { get { return MetaPactuada2025.HasValue ? MetaPactuada2025.Value * salarioMinimoExercicio4 * 12 : new Nullable<Decimal>(); } }

    }
}
