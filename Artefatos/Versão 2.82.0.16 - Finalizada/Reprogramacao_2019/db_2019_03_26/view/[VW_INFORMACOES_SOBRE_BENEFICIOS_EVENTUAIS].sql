USE [Dbpmas_quadrienal]
GO

/****** Object:  View [dbo].[VW_INFORMACOES_SOBRE_BENEFICIOS_EVENTUAIS]    Script Date: 26/03/2019 11:02:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[VW_INFORMACOES_SOBRE_BENEFICIOS_EVENTUAIS]
AS
SELECT 
	P.ID AS ID_PREFEITURA,
	MUN.ID AS ID_MUNICIPIO,
	MUN.ID_DRADS,
	MUN.NOME AS MUNICIPIO,
	DRA.NOME AS DRADS,
	COALESCE(MUN.ID_REGIAO_METROPOLITANA,-1) AS ID_REGIAO_METROPOLITANA,
	DRA.ID_MACRO_REGIAO,
	P.ID_NIVEL_GESTAO,
	(CASE WHEN P.POPULACAO <= 20000 THEN 1
		WHEN   P.POPULACAO <= 50000 THEN 2
		WHEN   P.POPULACAO <= 100000 THEN 3
		WHEN   P.POPULACAO <= 900000 THEN 4
		ELSE 5
	END) AS ID_PORTE,
	B.NOME AS BENEFICIO,
	B.ID AS ID_TIPO_BENEFICIO,
	PB.REGULAMENTACAO,
	ISNULL(CONVERT(CHAR(10),PB.DATA_PUBLICACAO_LEI,103),'') AS DATA_REGULAMENTACAO,
	PB.MEDIA_SEMESTRAL_BENEFICIARIOS,
	PB.MEDIA_SEMESTRAL_BENEFICIARIOS_CONCEDIDOS AS MEDIA_SEMESTRAL_BENEFICIOS_CONCEDIDOS
	--,CASE WHEN (BEO.ID_ORGAO_RESPONSAVEL = 1) THEN
	, CAST(CASE WHEN (SELECT 1 FROM TB_PREFEITURA_BENEFICIO_EVENTUALxORGAO_RESPONSAVEL WHERE PB.ID = ID_PREFEITURA_BENEFICIO_EVENTUAL AND ID_ORGAO_RESPONSAVEL = 1) = 1 THEN 1 ELSE 0 END AS BIT) AS ORGAO_GESTOR
	, CAST(CASE WHEN (SELECT 1 FROM TB_PREFEITURA_BENEFICIO_EVENTUALxORGAO_RESPONSAVEL WHERE PB.ID = ID_PREFEITURA_BENEFICIO_EVENTUAL AND ID_ORGAO_RESPONSAVEL = 2) = 1 THEN 1 ELSE 0 END AS BIT) AS CRAS
	, CAST(CASE WHEN (SELECT 1 FROM TB_PREFEITURA_BENEFICIO_EVENTUALxORGAO_RESPONSAVEL WHERE PB.ID = ID_PREFEITURA_BENEFICIO_EVENTUAL AND ID_ORGAO_RESPONSAVEL = 3) = 1 THEN 1 ELSE 0 END AS BIT) AS UNIDADE_PRIVADA
	, CAST(CASE WHEN (SELECT 1 FROM TB_PREFEITURA_BENEFICIO_EVENTUALxORGAO_RESPONSAVEL WHERE PB.ID = ID_PREFEITURA_BENEFICIO_EVENTUAL AND ID_ORGAO_RESPONSAVEL = 4) = 1 THEN 1 ELSE 0 END AS BIT) AS CREAS
	, CAST(CASE WHEN (SELECT 1 FROM TB_PREFEITURA_BENEFICIO_EVENTUALxORGAO_RESPONSAVEL WHERE PB.ID = ID_PREFEITURA_BENEFICIO_EVENTUAL AND ID_ORGAO_RESPONSAVEL = 5) = 1 THEN 1 ELSE 0 END AS BIT) AS CENTRO_POP
	, CAST(CASE WHEN (SELECT 1 FROM TB_PREFEITURA_BENEFICIO_EVENTUALxORGAO_RESPONSAVEL WHERE PB.ID = ID_PREFEITURA_BENEFICIO_EVENTUAL AND ID_ORGAO_RESPONSAVEL = 6) = 1 THEN 1 ELSE 0 END AS BIT) AS FUNDO_SOCIAL_SOLIDARIEDADE

	--, CAST(0 AS BIT) AS ORGAO_GESTOR
	--, CAST(0 AS BIT) AS CRAS
	--, CAST(0 AS BIT) AS UNIDADE_PRIVADA
	--, CAST(0 AS BIT) AS CREAS
	--, CAST(0 AS BIT) AS CENTRO_POP
	--, CAST(0 AS BIT) AS FUNDO_SOCIAL_SOLIDARIEDADE
	, PB1.VALOR_FMAS
	, PB1.VALOR_FUNDO_MUNICIPAL_SOLIDARIEDADE
	, PB1.VALOR_FEAS
	, PB1.VALOR_FUNDO_ESTADUAL_SOLIDARIEDADE
	, PB1.VALOR_FNAS
	, PB1.VALOR_ORCAMENTO_MUNICIPAL
	, (CASE WHEN F.ID = 3 THEN 'Aux�lio Financeiro e Material' ELSE F.NOME END) AS FORMA_AUXILIO
	--(case when pb.beneficiario_atendido_rede_socioassistencial = 1 then 'sim' else 'n�o' end) as integracao_servicos,
	, (
			CASE WHEN 
				(
					SELECT TOP 1 SPU.POSSUI_PROGRAMA_BENEFICIO 
					FROM [dbo].[TB_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO] SPU 
						INNER JOIN [dbo].[TB_PREFEITURA_BENEFICIO_EVENTUAL_SERVICOS] BS ON(BS.ID_SERVICOS_RECURSOS_FINANCEIROS_PUBLICO = SPU.ID )
					WHERE BS.ID_PREFEITURA_BENEFICIO_EVENTUAL = PB.ID) = 1 THEN 'Sim'
				WHEN 
				(
					SELECT TOP 1 SPU.POSSUI_PROGRAMA_BENEFICIO 
					FROM [dbo].[TB_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO] SPU
						INNER JOIN [dbo].[TB_PREFEITURA_BENEFICIO_EVENTUAL_SERVICOS] BS ON(BS.ID_SERVICOS_RECURSOS_FINANCEIROS_PRIVADO = SPU.ID)
					WHERE BS.ID_PREFEITURA_BENEFICIO_EVENTUAL = PB.ID) = 1 THEN 'Sim'
				WHEN 
				(
					SELECT  TOP 1 SPU.POSSUI_PROGRAMA_BENEFICIO 
					FROM [dbo].[TB_SERVICOS_RECURSOS_FINANCEIROS_CRAS] SPU
						INNER JOIN [dbo].[TB_PREFEITURA_BENEFICIO_EVENTUAL_SERVICOS] BS ON(BS.ID_SERVICOS_RECURSOS_FINANCEIROS_CRAS = SPU.ID)
					WHERE BS.ID_PREFEITURA_BENEFICIO_EVENTUAL = PB.ID) = 1 THEN 'Sim'
				WHEN 
				(
					SELECT TOP 1 SPU.POSSUI_PROGRAMA_BENEFICIO 
					FROM [dbo].[TB_SERVICOS_RECURSOS_FINANCEIROS_CREAS] SPU
						INNER JOIN [dbo].[TB_PREFEITURA_BENEFICIO_EVENTUAL_SERVICOS] BS ON(BS.ID_SERVICOS_RECURSOS_FINANCEIROS_CREAS = SPU.ID )
					WHERE BS.ID_PREFEITURA_BENEFICIO_EVENTUAL = PB.ID) = 1 THEN 'Sim'
				WHEN
				(
					SELECT TOP 1 SPU.POSSUI_PROGRAMA_BENEFICIO 
					FROM [dbo].[TB_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP] SPU
						INNER JOIN [dbo].[TB_PREFEITURA_BENEFICIO_EVENTUAL_SERVICOS] BS ON(BS.ID_SERVICOS_RECURSOS_FINANCEIROS_CENTRO_POP = SPU.ID )
					WHERE BS.ID_PREFEITURA_BENEFICIO_EVENTUAL = PB.ID) = 1 THEN 'Sim'
					ELSE'N�o' 
				END
	) AS INTEGRACAO_SERVICOS 
	, (SELECT count(*) from TB_PREFEITURA_BENEFICIO_EVENTUAL_SERVICOS S where S.ID_PREFEITURA_BENEFICIO_EVENTUAL = PB.ID) AS TOTAL_SERVICOS_ASSOCIADOS
	, PB1.EXERCICIO
	FROM TB_PREFEITURA_BENEFICIO_EVENTUAL PB
	INNER JOIN TB_PREFEITURA_BENEFICIO_EVENTUAL_RECURSOS_FINANCEIROS PB1 ON(PB.ID = PB1.ID_PREFEITURA_BENEFICIO_EVENTUAL)
	INNER JOIN TB_TIPO_BENEFICIO_EVENTUAL B ON(B.ID = PB.ID_TIPO_BENEFICIO_EVENTUAL)
	--INNER JOIN TB_PREFEITURA_BENEFICIO_EVENTUALxORGAO_RESPONSAVEL BEO ON PB.ID = BEO.ID_PREFEITURA_BENEFICIO_EVENTUAL
	INNER JOIN TB_PREFEITURA P ON(P.ID = PB.ID_PREFEITURA)
	INNER JOIN TB_FORMA_AUXILIO F ON(F.ID = PB.ID_FORMA_AUXILIO)
	JOIN DBSEDS.DBO.TB_MUNICIPIOS MUN (NOLOCK) ON(P.ID_MUNICIPIO = MUN.ID)
	JOIN DBSEDS.DBO.TB_DRADS DRA (NOLOCK) ON(MUN.ID_DRADS = DRA.ID)
	WHERE MEDIA_SEMESTRAL_BENEFICIARIOS IS NOT NULL


GO


