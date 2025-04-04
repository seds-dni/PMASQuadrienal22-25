USE [Dbpmas_quadrienal_DESENV]
GO
/****** Object:  StoredProcedure [dbo].[PR_LEI_ORCAMENTARIA_2016]    Script Date: 10/04/2019 16:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[PR_LEI_ORCAMENTARIA_2016] 
	@ID_PREFEITURA  INT 
AS
	SELECT ID_PREFEITURA,
		VALOR_APROVADO,
		LEI,
		DATA_PUBLICACAO,
		cast(0 as bit) OutrosRecursos,
		0.0 ValorRecursosHumanos,
		0.0 ValorManutencaoEquipamentos,
		0.0 ValorConstrucaoUnidades,
		0.0 ValorAquisicaoBens,
		0.0 ValorRecursosFMAS,
		0.0 ValorRecursosNaoAlocadosFMAS,
		'' AS NomeVeiculoComunicacao,
		'' AS ComentariosOrgaoGestor,
		 Exercicio = 2016
	FROM DBPMAS2017.dbo.TB_LEI_ORCAMENTARIA WHERE ID_PREFEITURA = @ID_PREFEITURA


	